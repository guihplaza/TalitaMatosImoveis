using Alien.Web.Models;
using BusinessLogic;
using Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Alien.Web.Controllers
{
    public class ImovelController : Controller
    {
        //Define uma instância de IHostingEnvironment
        IHostingEnvironment _appEnvironment;
        //Injeta a instância no construtor para poder usar os recursos

        private readonly IBaseServices<AlienDB_Tipo_Imovel> _ITipoImovelServices;
        private readonly IBaseServices<AlienDB_Imovel> _IImovelServices;
        private readonly IBaseServices<AlienDB_Midia> _IMidiaServices;
        private readonly IBaseServices<AlienDB_Regiao> _IRegiaoServices;
        private readonly IBaseServices<AlienDB_Usuario_Sistema> _IUsuarioServices;
        public bool validaLogin = false;
        public ImovelController(IBaseServices<AlienDB_Usuario_Sistema> IUsuarioServices, IBaseServices<AlienDB_Regiao> IRegiaoServices, IBaseServices<AlienDB_Tipo_Imovel> ITipoImovelServices, IBaseServices<AlienDB_Midia> IMidiaServices, IBaseServices<AlienDB_Imovel> IImovelServices, IHostingEnvironment env)
        {
            _ITipoImovelServices = ITipoImovelServices;

            _IRegiaoServices = IRegiaoServices;

            _IImovelServices = IImovelServices;

            _IMidiaServices = IMidiaServices;

            _IUsuarioServices = IUsuarioServices;

            _appEnvironment = env;
        }

        private async Task verificaUsuarioLogado()
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (idUsuario == null || idUsuario == 0)
            {
                validaLogin = false;
            }
            else
            {

                validaLogin = true;
                int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;

                //ViewData["Nome"] = _IUsuarioServices.Listar(w => w.Id == idUsuario).Select(c => c.Nom_Completo).SingleOrDefault();
            }
        }
        private async Task carregarViewBagRegioes()
        {

            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;

            var lista = await _IRegiaoServices.Listar(w => w.Id_Empresa == idEmpresa);
            ViewBag.Regiao = lista.Select(c => new SelectListItem()
            {
                Text = c.Descricao,
                Value = c.Id.ToString()

            }).ToList();

        }
        private async Task carregarViewBag()
        {
            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;

            var lista = await _ITipoImovelServices.Listar(w => w.Id_Empresa == idEmpresa);
            ViewBag.TipoImovel = lista.Select(c => new SelectListItem()
            {
                Text = c.Descricao,
                Value = c.Id.ToString()

            }).ToList();
        }

        // GET: ImovelController
        public async Task<IActionResult> Index(string ordem, string filtroAtual, string filtro, int? pagina = 1)
        {

            ViewData["NomeParm"] = String.IsNullOrEmpty(ordem) ? "nome_desc" : "";
            ViewData["DataParm"] = ordem == "Data" ? "data_desc" : "Data";
            
            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;

            /*VERIFICAR SE O USUARIO ESTÁ LOGADO NO SISTEMA*/
            await verificaUsuarioLogado();
            if (validaLogin == false)
            {
                return Redirect("~/Home/IndexModal");
            }
            /*FIM DA VALIDAÇÃO*/

            await carregarViewBagRegioes();
            await carregarViewBag();

            const int itensPorPagina = 10;

            int numeroPagina = (pagina ?? 1);

            //var lista = ;
            var model = await _IImovelServices.ListarPagedList(w => w.Id_Empresa == idEmpresa, numeroPagina, itensPorPagina);

            if (filtro != null)
            {
                pagina = 1;
            }
            else
            {
                filtro = filtroAtual;
            }

            ViewData["filtroAtual"] = filtro;
            ViewData["filtroImovel"] = filtro;


            if (!String.IsNullOrEmpty(filtro))
            {
                model = await _IImovelServices.ListarPagedList(s => s.Endereco.Contains(filtro)
                                      || s.Codigo_chave.ToString().Contains(filtro), numeroPagina, itensPorPagina);
            }

            return View(model);
        }

        // GET: ImovelController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            await carregarViewBagRegioes();
            await carregarViewBag();
            return View();
        }

        // GET: ImovelController/Create
        public async Task<IActionResult> Create()
        {
            /*VERIFICAR SE O USUARIO ESTÁ LOGADO NO SISTEMA*/
            await verificaUsuarioLogado();
            if (validaLogin == false)
            {
                return Redirect("~/Home/IndexModal");
            }
            /*FIM DA VALIDAÇÃO*/
            await carregarViewBag();
            await carregarViewBagRegioes();
 
            return View();
        }

        // POST: ImovelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlienDB_Imovel imovel)
        {

            await carregarViewBag();
            await carregarViewBagRegioes();

            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;
            imovel.Id_Empresa = idEmpresa;

            try
            {
                await _IImovelServices.Adicionar(imovel);
                return RedirectToRoute(new { controller = "Imovel", action = "Upload", id = imovel.Id });
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(imovel);

        }

        // GET: ImovelController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            await carregarViewBag();
            await carregarViewBagRegioes();
            var imovel = await _IImovelServices.GetById(s => s.Id == id);
            return View(imovel);
        }

        // POST: ImovelController/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id, IFormCollection collection)
        {
            /*VERIFICAR SE O USUARIO ESTÁ LOGADO NO SISTEMA*/
            await verificaUsuarioLogado();
            if (validaLogin == false)
            {
                return Redirect("~/Home/IndexModal");
            }
            /*FIM DA VALIDAÇÃO*/

            await carregarViewBag();
            await carregarViewBagRegioes();
            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;

            var atualizaImovel = await _IImovelServices.GetById(s => s.Id == id);
            if (await TryUpdateModelAsync<AlienDB_Imovel>(
                atualizaImovel,
                "",
                s => s.Id_Tipo_Imovel, s => s.Id_Regiao, s => s.Codigo_chave, s => s.Endereco, s => s.Complemento, s => s.Bairro, s => s.Cidade, 
                s => s.Qtd_Dormitorios, s => s.Qtd_Sala_estar,
                s => s.Qtd_Sala_estar, s => s.Qtd_Banheiro, s => s.Qtd_Lavanderia, s => s.Qtd_Edicula, s => s.Qtd_Suite, s => s.Qtd_Copa, 
                s => s.Qtd_Carro_Garagem, s => s.Qtd_Dispensa,
                s => s.Qtd_Quintal, s => s.Qtd_Aluguel_Advogado, s => s.Qtd_Aluguel_Pintura, s => s.Nro_Parcela_Inicio_Aluguel_Imobiliaria, 
                s => s.Observacoes, s => s.Valor_IPTU,
                s => s.Valor_condominio, s => s.Valor_Taxa_Administrativa, s => s.Valor_aluguel, s => s.Condicoes_Imovel, 
                s => s.Dat_saida_locacao, s => s.Dat_Inclui, s => s.Flg_destaque, s => s.Flg_status))
            {
                try
                {
                    await _IImovelServices.Alterar(atualizaImovel);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(atualizaImovel);
        }

        // GET: ImovelController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            /*VERIFICAR SE O USUARIO ESTÁ LOGADO NO SISTEMA*/
            await verificaUsuarioLogado();

            if (validaLogin == false)
            {
                return Redirect("~/Home/IndexModal");
            }
            /*FIM DA VALIDAÇÃO*/
            var imovel = await _IImovelServices.GetById(s => s.Id == id);
            return View(imovel);
        }

        // POST: ImovelController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*VERIFICAR SE O USUARIO ESTÁ LOGADO NO SISTEMA*/
            await verificaUsuarioLogado();

            if (validaLogin == false)
            {
                return Redirect("~/Home/IndexModal");
            }
            /*FIM DA VALIDAÇÃO*/
            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;
            var atualizaImovel = await _IImovelServices.GetById(s => s.Id == id);

            try
            {
                await _IImovelServices.Excluir(atualizaImovel);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                return View();
            }
        }
        public async Task<IActionResult> Upload(int id)
        {

            /*VERIFICAR SE O USUARIO ESTÁ LOGADO NO SISTEMA*/
            await verificaUsuarioLogado();
           
            if (validaLogin == false)
            {
                return Redirect("~/Home/IndexModal");
            }
            /*FIM DA VALIDAÇÃO*/

            HttpContext.Session.SetInt32("IdImovel", id);
            //var imovel = _IMidiaServices.Listar(m => m.Id_Empresa == idEmpresa  && m.Id_Imovel == id).FirstOrDefault();

            return View();
        }

        [HttpPost, ActionName("Upload")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImagem(int id, IList<IFormFile> arquivos)
        {
            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;
            IFormFile imagemEnviada = arquivos.FirstOrDefault();

            var idImovel = HttpContext.Session.GetInt32("IdImovel");

            foreach (var arquivo in arquivos)
            {
                //verifica se existem arquivos 
                if (arquivo == null || arquivo.Length == 0)
                {
                    //retorna a viewdata com erro
                    ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                    return View(ViewData);
                }

                if (imagemEnviada != null || imagemEnviada.ContentType.ToLower().StartsWith("image/"))
                {
                    MemoryStream ms = new MemoryStream();
                    imagemEnviada.OpenReadStream().CopyTo(ms);

                    AlienDB_Midia imagemEntity = new AlienDB_Midia()
                    {
                        Nome = imagemEnviada.FileName,
                        ImagemByte = ms.ToArray(),
                        Id_Empresa = idEmpresa,
                        Id_Imovel = Convert.ToInt32(idImovel),
                        ContentType = imagemEnviada.ContentType
                    };

                    await _IMidiaServices.Adicionar(imagemEntity);

                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<FileStreamResult> GaleriaDois(int id)
        {
            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;

            var lista = await _IMidiaServices.Listar(m => m.Id_Empresa == idEmpresa && m.Id_Imovel == id);
            AlienDB_Midia imagem = lista.FirstOrDefault();
            MemoryStream ms = new MemoryStream(imagem.ImagemByte);


            StreamReader reader = new StreamReader(ms);
            string text = reader.ReadToEnd();

            return new FileStreamResult(ms, imagem.ContentType);

            /*
            
                foreach (DataRow item in dtTemp.Rows)
                {
                    var itemLista = new Models.PessoaView();
                    itemLista.SeqPessoa = Convert.ToInt32(item["SEQ_PESSOA"].ToString());
                    itemLista.NomPessoa = Convert.ToString(item["NOM_PESSOA"]);
                    itemLista.Imagem = Convert.ToBase64String((byte[])item["IMG_FOTO"]);
                    
                    lista.Add(itemLista);

                }
            */
        }

        [HttpGet]
        public async Task<IActionResult> Galeria(int id)
        {
            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;
            var idImovel = HttpContext.Session.GetInt32("IdImovel");
            var midia = await _IMidiaServices.Listar(m => m.Id_Empresa == idEmpresa && m.Id_Imovel == id);

            //.Select(m => Convert.ToBase64String(m.ImagemByte))
            //MemoryStream ms = new MemoryStream(midia.ImagemByte);
            //return new FileStreamResult(ms, midia.ContentType);

            return View(midia);
        }

        public async Task<IActionResult> MaisFotos(int id)
        {
            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;
            var idImovel = HttpContext.Session.GetInt32("IdImovel");
            var midia = await _IMidiaServices.Listar(m => m.Id_Empresa == idEmpresa && m.Id_Imovel == id);

            //.Select(m => Convert.ToBase64String(m.ImagemByte))
            //MemoryStream ms = new MemoryStream(midia.ImagemByte);
            //return new FileStreamResult(ms, midia.ContentType);

            return View(midia);
        }

        public async Task<IActionResult> EnviarArquivo(int? id, List<IFormFile> arquivos)
        {

            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;

            var idImovel = HttpContext.Session.GetInt32("IdImovel");

            //var imovel = _IMidiaServices.Listar(m => m.Id_Empresa == idEmpresa && m.Id_Imovel == id).FirstOrDefault();

            long tamanhoArquivos = arquivos.Sum(f => f.Length);
            // caminho completo do arquivo na localização temporária
            var caminhoArquivo = Path.GetTempFileName();

            // processa o arquivo enviado
            var objImove = await _IImovelServices.GetById(c => c.Id == idImovel);
            //percorre a lista de arquivos selecionados
            foreach (var arquivo in arquivos)
            {
                //verifica se existem arquivos 
                if (arquivo == null || arquivo.Length == 0)
                {
                    //retorna a viewdata com erro
                    ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                    return View(ViewData);
                }

                // < define a pasta onde vamos salvar os arquivos >
                string pasta = "Arquivos_Imoveis";
                // Define um nome para o arquivo enviado incluindo o sufixo obtido de milesegundos
                string nomeArquivo = "Foto" + DateTime.Now.Millisecond.ToString() + "_" + objImove.Endereco;

                //verifica qual o tipo de arquivo : jpg, gif, png, pdf ou tmp
                if (arquivo.FileName.Contains(".jpg"))
                    nomeArquivo += ".jpg";
                if (arquivo.FileName.Contains(".jpeg"))
                    nomeArquivo += ".jpeg";
                else if (arquivo.FileName.Contains(".gif"))
                    nomeArquivo += ".gif";
                else if (arquivo.FileName.Contains(".png"))
                    nomeArquivo += ".png";
                else if (arquivo.FileName.Contains(".pdf"))
                    nomeArquivo += ".pdf";
                else
                    nomeArquivo += ".jpg";

                //< obtém o caminho físico da pasta wwwroot >
                string caminho_WebRoot = _appEnvironment.WebRootPath;
                // monta o caminho onde vamos salvar o arquivo :  ~\wwwroot\Arquivos\Arquivos_Usuario\Recebidos
                string caminhoDestinoArquivo = caminho_WebRoot + "\\Arquivos\\" + pasta + "\\";
                // incluir a pasta Recebidos e o nome do arquiov enviado : ~\wwwroot\Arquivos\Arquivos_Usuario\Recebidos\
                string caminhoDestinoArquivoOriginal = caminhoDestinoArquivo + "\\Recebidos\\" + nomeArquivo;
                string caminhoBanco = "\\Arquivos\\" + pasta + "\\" + "\\Recebidos\\" + nomeArquivo;
                //copia o arquivo para o local de destino original
                using (var stream = new FileStream(caminhoDestinoArquivoOriginal, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }

                //SALVAR CAMINHO NO BANCO

                IFormFile imagemEnviada = arquivos.FirstOrDefault();



                if (imagemEnviada != null || imagemEnviada.ContentType.ToLower().StartsWith("image/"))
                {
                    MemoryStream ms = new MemoryStream();
                    imagemEnviada.OpenReadStream().CopyTo(ms);

                    AlienDB_Midia imagemEntity = new AlienDB_Midia()
                    {
                        Caminho = caminhoBanco,
                        Nome = imagemEnviada.FileName,
                        //ImagemByte = ms.ToArray(),
                        Id_Empresa = idEmpresa,
                        Id_Imovel = Convert.ToInt32(idImovel),
                        ContentType = imagemEnviada.ContentType
                    };

                    await _IMidiaServices.Adicionar(imagemEntity);
                }
            }

            return RedirectToAction("Index");
        }

        private async Task carregarViewBagImoveis()
        {
            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;

            var lista = await _IImovelServices.Listar(w => w.Id_Empresa == idEmpresa);
            ViewBag.Imoveis = lista.Select(c => new SelectListItem()
            {
                Text = c.Endereco,
                
                Value = c.Id.ToString()

            }).ToList();
        }
        // GET: MatriculaController
        public async Task<IActionResult> MaisImoveis(int pageNumber = 1, int pageSize = 10, int pageIndex = 1)
        {
            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;

            //var imoveis = await _IImovelServices.Listar(w => w.Id_Empresa == idEmpresa, pageNumber, pageSize);
            //var qtde = await _IMidiaServices.Listar(w => w.Id_Empresa == idEmpresa && w.Imovel.Flg_status != "Alugado");
            var listaForms = await _IMidiaServices.Listar(w => w.Id_Empresa == idEmpresa && w.Imovel.Flg_status != "Alugado" && w.ImagemPrincipal == true);

            var pag = PaginatedList<AlienDB_Midia>.Create(listaForms, pageNumber, pageSize);
            pag.pageNumber = pageNumber;
            pag.pageSize = pageSize;
            pag.pageIndex = pageIndex + 1;

            /*
            foreach (var n in imoveis)
            {
                var model = await _IMidiaServices.GetById(w => w.Id_Empresa == idEmpresa && w.Id_Imovel == n.Id && w.Imovel.Flg_status != "Alugado");

                listaForms.Add(model);
            }
            */
            return View(pag);
        }

    }
}
