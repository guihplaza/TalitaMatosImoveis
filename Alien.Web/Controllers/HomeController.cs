using Alien.Web.Models;
using BusinessLogic;
using Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Alien.Web.Controllers
{
    public class HomeController : Controller
    {
        //Define uma instância de IHostingEnvironment
        IHostingEnvironment _appEnvironment;
        //Injeta a instância no construtor para poder usar os recursos

        private readonly IBaseServices<AlienDB_Tipo_Imovel> _ITipoImovelServices;
        private readonly IBaseServices<AlienDB_Imovel> _IImovelServices;
        private readonly IBaseServices<AlienDB_Midia> _IMidiaServices;

        public HomeController(IBaseServices<AlienDB_Tipo_Imovel> ITipoImovelServices, 
            IBaseServices<AlienDB_Midia> IMidiaServices, 
            IBaseServices<AlienDB_Imovel> IImovelServices, 
            IHostingEnvironment env)
        {
            _ITipoImovelServices = ITipoImovelServices;

            _IImovelServices = IImovelServices;

            _IMidiaServices = IMidiaServices;

            _appEnvironment = env;
        }

        public IActionResult Contact()
        {
           

            return View();
        }

        public IActionResult TrabalheConosco()
        {
          

            return View();
        }

        public IActionResult OndeEstamos()
        {
            

            return View();
        }


        public IActionResult RedeSocial()
        {
            

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> IndexModal(int pageNumber = 1, int pageSize = 8, int? pageIndex = null)
        {
            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;
            var listaForms = new List<AlienDB_Midia>();
            bool bInformouIndex = pageIndex.HasValue;

            if (!pageIndex.HasValue)
                pageIndex = 1;

            var imoveis = await _IImovelServices.Listar(w => w.Id_Empresa == idEmpresa && w.Flg_destaque == "SIM", pageNumber, pageSize);



            foreach (var n in imoveis)
            {
                var model = await _IMidiaServices.GetById(w => w.Id_Empresa == idEmpresa && w.Id_Imovel == n.Id && w.Imovel.Flg_destaque == "SIM");

                listaForms.Add(model);

            }

            var pag = PaginatedList<AlienDB_Midia>.Create(listaForms, pageNumber, pageSize);
            pag.pageNumber = pageNumber;
            pag.pageSize = pageSize;

            if(bInformouIndex)
                pag.pageIndex = pageIndex.Value + 1;
            else
                pag.pageIndex = pageIndex.Value;
            // pag.pesquisa = pesquisa;

            return View(pag);
        }

        public async Task<IActionResult> Pesquisa(string filtroVenda, int? filtroValor, string? filtroRegiao, string? filtroTipo, int pageNumber = 1, int pageSize = 8, int pageIndex = 1)
        {
            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;

            var listaForms = await _IMidiaServices.Listar(w => w.Id_Empresa == idEmpresa &&
                                                               w.ImagemPrincipal &&
                                                               (String.IsNullOrWhiteSpace(filtroRegiao) ? true : w.Imovel.Regiao.Descricao.ToLower() == filtroRegiao.ToLower()) &&
                                                               (String.IsNullOrWhiteSpace(filtroTipo) ? true : w.Imovel.TipoImovel.Descricao.ToLower() == filtroTipo.ToLower()) &&
                                                               (filtroValor.HasValue ? w.Imovel.Valor_aluguel.Value >= filtroValor && w.Imovel.Valor_aluguel.Value <= (filtroValor + 100) : true)
                                                         );

            var pag = PaginatedList<AlienDB_Midia>.Create(listaForms, pageNumber, pageSize);
            pag.pageIndex = pageIndex;
            pag.pageNumber = pageNumber;
            pag.pageSize = pageSize;
            pag.filtroVenda = filtroVenda;
            pag.filtroValor = filtroValor;
            pag.filtroRegiao = filtroRegiao;
            pag.filtroTipo = filtroTipo;
            
            return View(pag);

            #region Dont Touch - Gui Gui Code kkkk
            /*
            if (!String.IsNullOrEmpty(filtroRegiao) && !String.IsNullOrEmpty(filtroTipo) && filtroValor != null && filtroVenda != "Venda")
            {
                var busca = await _IImovelServices.Listar(s => s.Regiao.Descricao.ToLower() == filtroRegiao.ToLower() && s.TipoImovel.Descricao.ToLower() == filtroTipo.ToLower()
                //s.Regiao.Descricao.Contains(filtroRegiao)
                //&& s.TipoImovel.Descricao.Contains(filtroTipo)
                && s.Valor_aluguel.Value >= filtroValor && s.Valor_aluguel.Value <= (filtroValor+100));

                var listaForms = new List<AlienDB_Midia>();

                foreach (var n in busca)
                {
                    var model = await _IMidiaServices.GetById(w => w.Id_Empresa == idEmpresa && w.Id_Imovel == n.Id);

                    listaForms.Add(model);
                }


                return View(listaForms);
            }
            else if (filtroVenda == "Venda")
            {
                var busca = await _IImovelServices.Listar(s => s.Flg_status.Contains(filtroVenda) || s.Regiao.Descricao.ToLower() == filtroRegiao.ToLower()
                && s.TipoImovel.Descricao.ToLower() == filtroTipo.ToLower()//s.TipoImovel.Descricao.Contains(filtroTipo)
                && s.Valor_aluguel.Value >= filtroValor && s.Valor_aluguel.Value <= (filtroValor + 100));

                var listaForms = new List<AlienDB_Midia>();

                foreach (var n in busca)
                {
                    var model = await _IMidiaServices.GetById(w => w.Id_Empresa == idEmpresa && w.Id_Imovel == n.Id);

                    listaForms.Add(model);
                }

                return View(listaForms);
            }
            else if (!String.IsNullOrEmpty(filtroRegiao) || !String.IsNullOrEmpty(filtroTipo) && filtroVenda != "Venda")
            {
                //var busca = await _IImovelServices.Listar(s => s.Regiao.Descricao.Contains(filtroRegiao)
                //&& s.TipoImovel.Descricao.Contains(filtroTipo));

                var listaForms = await _IMidiaServices.Listar(w => w.Id_Empresa == idEmpresa && 
                                                                   w.Imovel.Regiao.Descricao.ToLower() == filtroRegiao.ToLower() &&
                                                                   w.Imovel.TipoImovel.Descricao.ToLower() == filtroTipo.ToLower());

                //var busca = await _IImovelServices.Listar(s => s.Regiao.Descricao.ToLower() == filtroRegiao.ToLower() && s.TipoImovel.Descricao.ToLower() == filtroTipo.ToLower());

                //var listaForms = new List<AlienDB_Midia>();

                //foreach (var n in busca)
                //{
                //    var model = await _IMidiaServices.GetById(w => w.Id_Empresa == idEmpresa && w.Id_Imovel == n.Id);
                //
                //    listaForms.Add(model);
                //}


                return View(listaForms);

            }
            else if (filtroValor != null)
                {
                     var busca = await _IImovelServices.Listar(s => s.Valor_aluguel.Value >= filtroValor && s.Valor_aluguel.Value <= (filtroValor + 100));

                        var listaForms = new List<AlienDB_Midia>();

                        foreach (var n in busca)
                        {
                            var model = await _IMidiaServices.GetById(w => w.Id_Empresa == idEmpresa && w.Id_Imovel == n.Id);

                            listaForms.Add(model);
                        }

                     return View(listaForms);
                }



            

            return View();*/
            #endregion
        }

        public async Task atualizarFotoPrincipal()
        {
            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;

            var imoveis = await _IImovelServices.Listar(w => w.Id_Empresa == idEmpresa);

            var listaForms = new List<AlienDB_Midia>();

            foreach (var n in imoveis)
            { 
                //atualizar a primeira foto do imóvel como sendo a foto principal.
                var modelImovelFotoPrincipal = await _IMidiaServices.Listar(w => w.Id_Empresa == idEmpresa && w.Id_Imovel == n.Id);
                var imagemPrincipal = modelImovelFotoPrincipal.OrderBy(w => w.Id).FirstOrDefault();
                if (imagemPrincipal != null)
                {
                    imagemPrincipal.ImagemPrincipal = true;
                    await _IMidiaServices.Alterar(imagemPrincipal);
                }
            }
        }
    }
}
