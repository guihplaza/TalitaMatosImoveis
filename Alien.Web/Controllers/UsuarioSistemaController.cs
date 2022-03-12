using BusinessLogic;
using Castle.Core.Internal;
using Domain;
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
    public class UsuarioSistemaController : Controller
    {
      
        //Injeta a instância no construtor para poder usar os recursos

        private readonly IBaseServices<AlienDB_Usuario_Sistema> _IUsuarioServices;
        public bool validaLogin = false;


        public UsuarioSistemaController(IBaseServices<AlienDB_Usuario_Sistema> IUsuarioServices)
        {
            _IUsuarioServices = IUsuarioServices;

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

                var usuario = await _IUsuarioServices.GetById(w => w.Id == idUsuario);
                ViewData["Nome"] = usuario.Nom_Completo;
            }
        }
        // GET: UsuarioSistemaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioSistemaController/Edit/5
        public async Task<ActionResult> Login(string filtroUs, string filtroSe)
        {            
            int idEmpresa = HttpContext.Session.GetInt32("Id_Empresa").HasValue ? HttpContext.Session.GetInt32("Id_Empresa").Value : 1;

            if (!String.IsNullOrEmpty(filtroUs) && !String.IsNullOrEmpty(filtroSe))
            {
                var lista = await _IUsuarioServices.Listar(w => w.Id_Empresa == idEmpresa &&
                                               w.Nom_Login.ToLower() == filtroUs.ToLower() &&
                                               w.Des_Senha.ToLower() == filtroSe.ToLower());

                AlienDB_Usuario_Sistema usuario = lista.FirstOrDefault();

                if (usuario == null)
                {
                    ViewData["Erro"] = "Usuário ou Senha Inválido!";
                    return View();
                }
                else
                {
                    HttpContext.Session.SetInt32("IdUsuario", usuario.Id);
                    return Redirect("~/Imovel/Index");
                }

            }
            return View();

        }
    }
}
