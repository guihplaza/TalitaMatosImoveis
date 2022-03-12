using Domain;
using BusinessLogic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alien.Web.Controllers
{
    public class EmpreendimentosController : Controller
    {
        IHostingEnvironment _appEnvironment;
        //Injeta a instância no construtor para poder usar os recursos

        private readonly IBaseServices<AlienDB_Tipo_Imovel> _ITipoImovelServices;
        private readonly IBaseServices<AlienDB_Imovel> _IImovelServices;
        private readonly IBaseServices<AlienDB_Midia> _IMidiaServices;
        private readonly IBaseServices<AlienDB_Empreendimentos> _IEmpreendimentoServices;
        private readonly IBaseServices<AlienDB_Regiao> _IRegiaoServices;
        private readonly IBaseServices<AlienDB_Usuario_Sistema> _IUsuarioServices;
        public bool validaLogin = false;

        public EmpreendimentosController(IBaseServices<AlienDB_Regiao> IRegiaoServices, IBaseServices<AlienDB_Empreendimentos> IEmpreendimentoServices, IBaseServices<AlienDB_Tipo_Imovel> ITipoImovelServices, IBaseServices<AlienDB_Midia> IMidiaServices, IBaseServices<AlienDB_Imovel> IImovelServices, IHostingEnvironment env)
        {
            _IEmpreendimentoServices = IEmpreendimentoServices;

            _ITipoImovelServices = ITipoImovelServices;

            _IRegiaoServices = IRegiaoServices;

            _IImovelServices = IImovelServices;

            _IMidiaServices = IMidiaServices;

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

        private async Task carregarViewBagRegioes()
        {
            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;

            var lstReg = await _IRegiaoServices.Listar(w => w.Id_Empresa == idEmpresa);

            ViewBag.Regiao = lstReg.Select(c => new SelectListItem()
            {
                Text = c.Descricao,
                Value = c.Id.ToString()

            }).ToList();

        }

        // GET: EmpreendimentoController
        public async Task<IActionResult> Index()
        {
            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;
            await carregarViewBag();
            await carregarViewBagRegioes();
            return View( await _IEmpreendimentoServices.Listar(w=>w.Id_Empresa == idEmpresa));
        }

        public async Task<IActionResult> Adm()
        {
            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;
            await carregarViewBag();
            await carregarViewBagRegioes();
            return View(await _IEmpreendimentoServices.Listar(w => w.Id_Empresa == idEmpresa));
        }

        public async Task<ActionResult> Consultar()
        {
            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;
            await carregarViewBag();
            await carregarViewBagRegioes();
            return View(await _IEmpreendimentoServices.Listar(w => w.Id_Empresa == idEmpresa));
        }

        // GET: EmpreendimentoController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View();
        }

        // GET: EmpreendimentoController/Create
        public async Task<ActionResult> Create()
        {
            /*VERIFICAR SE O USUARIO ESTÁ LOGADO NO SISTEMA*/
            await verificaUsuarioLogado();
            if (validaLogin == false)
            {
                return Redirect("~/Home/IndexModal");
            }
            /*FIM DA VALIDAÇÃO*/
            await carregarViewBagRegioes();
            await carregarViewBag();
            return View();
        }

        // POST: EmpreendimentoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlienDB_Empreendimentos empreendimento)
        {
            await carregarViewBag();
            await carregarViewBagRegioes();
            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;
            
            empreendimento.Id_Empresa = idEmpresa;

            try
            {
                await _IEmpreendimentoServices.Adicionar(empreendimento);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(empreendimento);
        }

        // GET: EmpreendimentoController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View();
        }

        // POST: EmpreendimentoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmpreendimentoController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View();
        }

        // POST: EmpreendimentoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
