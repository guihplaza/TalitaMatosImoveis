using BusinessLogic;
using Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Alien.Web.Controllers
{
    public class CadastreSeuImovelController : Controller
    {

        //Define uma instância de IHostingEnvironment
        IHostingEnvironment _appEnvironment;
        //Injeta a instância no construtor para poder usar os recursos

        private readonly IBaseServices<AlienDB_Tipo_Imovel> _ITipoImovelServices;
        private readonly IBaseServices<AlienDB_Imovel> _IImovelServices;
        private readonly IBaseServices<AlienDB_Midia> _IMidiaServices;
        private readonly IBaseServices<AlienDB_Cadastre_Seu_Imovel> _ICadastreSeuImovelServices;
        private readonly IBaseServices<AlienDB_Usuario_Sistema> _IUsuarioServices;
        public bool validaLogin = false;
        public CadastreSeuImovelController(IBaseServices<AlienDB_Usuario_Sistema> IUsuarioServices, IBaseServices<AlienDB_Cadastre_Seu_Imovel> ITCadastreSeuImovelServices, IBaseServices<AlienDB_Tipo_Imovel> ITipoImovelServices, IBaseServices<AlienDB_Midia> IMidiaServices, IBaseServices<AlienDB_Imovel> IImovelServices, IHostingEnvironment env)
        {
            _ITipoImovelServices = ITipoImovelServices;

            _IImovelServices = IImovelServices;

            _IMidiaServices = IMidiaServices;

            _ICadastreSeuImovelServices = ITCadastreSeuImovelServices;

            _IUsuarioServices = IUsuarioServices;

            _appEnvironment = env;
        }
        // GET: CadastreSeuImovel

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

                var user = await _IUsuarioServices.GetById(w => w.Id == idUsuario);
                ViewData["Nome"] = user.Nom_Completo;

            }

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


        // GET: EmpreendimentoController
        public async Task<IActionResult> Index()
        {
            /*VERIFICAR SE O USUARIO ESTÁ LOGADO NO SISTEMA*/
            await verificaUsuarioLogado();
            if (validaLogin == false)
            {
                return Redirect("~/Home/IndexModal");
            }
            /*FIM DA VALIDAÇÃO*/
            return View(_ICadastreSeuImovelServices.Listar());
        }

        // GET: CadastreSeuImovel/Create
        public async Task<ActionResult> Create()
        {
            await carregarViewBag();
            return View();
        }

        // POST: CadastreSeuImovel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AlienDB_Cadastre_Seu_Imovel cadImovel)
        {

            var response = Request.Form["g-recaptcha-response"];
            string secretKey = "6Lf6y9scAAAAANdfnmYEKG11RB8qyGp9HDhPSpcj";
            var cliente = new WebClient();
            var resultado = cliente.DownloadString(
                string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",
                secretKey, response));
            var obj = JObject.Parse(resultado);

            var status = (bool)obj.SelectToken("success");
            if(status != false)
            { 
                await carregarViewBag();
                int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;
                cadImovel.Id_Empresa = idEmpresa;

                try
                {
                    await _ICadastreSeuImovelServices.Adicionar(cadImovel);
                    return RedirectToRoute(new { controller = "Home", action = "IndexModal"}); 

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(cadImovel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            /*VERIFICAR SE O USUARIO ESTÁ LOGADO NO SISTEMA*/
            await verificaUsuarioLogado();
            if (validaLogin == false)
            {
                return Redirect("~/Home/IndexModal");
            }
            /*FIM DA VALIDAÇÃO*/

            return View(await _ICadastreSeuImovelServices.GetById(s => s.Id == id));
        }

        // POST: CursoController/Delete/5
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
            //var curso = await _context.Cursos.AsNoTracking().SingleOrDefaultAsync(m => m.CursoID == id);

            var cadastro = await _ICadastreSeuImovelServices.GetById(m => m.Id == id);
            if (cadastro == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                await _ICadastreSeuImovelServices.Excluir(cadastro);
                return RedirectToAction("Index");
            }
            catch (DbUpdateException e)
            {
                //Logar o erro
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }
    }
}
