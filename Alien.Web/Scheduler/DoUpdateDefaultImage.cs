using BusinessLogic;
using DNTScheduler.Core.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alien.Web.Scheduler
{
    public class DoUpdateDefaultImage : IScheduledTask
    {
        private readonly IBaseServices<AlienDB_Imovel> _IImovelServices;
        private readonly IBaseServices<AlienDB_Midia> _IMidiaServices;

        public DoUpdateDefaultImage(IBaseServices<AlienDB_Imovel> IImovelServices,
                                    IBaseServices<AlienDB_Midia> IMidiaServices)
        {
            this._IImovelServices = IImovelServices;
            this._IMidiaServices  = IMidiaServices;
        }

        public bool IsShuttingDown { get; set; }

        public async Task RunAsync()
        {
            if (this.IsShuttingDown)
            {
                return;
            }

            this.IsShuttingDown = true;

            var listaClasseImovelComImagemPrincipal = await _IMidiaServices.Listar(w => w.ImagemPrincipal);
            var listaImovelComImagemPrincipal1 = listaClasseImovelComImagemPrincipal.Select(w=>w.Id_Imovel);

            //Remove os imoveis que já possuem imovel principal setado
            var imoveis = await _IImovelServices.Listar(w=> !listaImovelComImagemPrincipal1.Contains(w.Id) );

            if (imoveis != null && imoveis.Count() > 0)
            {
                var listaForms = new List<AlienDB_Midia>();

                foreach (var n in imoveis)
                {
                    //atualizar a primeira foto do imóvel como sendo a foto principal.
                    var modelImovelFotoPrincipal = await _IMidiaServices.Listar(w => w.Id_Imovel == n.Id);
                    var imagemPrincipal = modelImovelFotoPrincipal.OrderBy(w => w.Id).FirstOrDefault();
                    if (imagemPrincipal != null)
                    {
                        imagemPrincipal.ImagemPrincipal = true;
                        await _IMidiaServices.Alterar(imagemPrincipal);
                    }
                }
            }

            this.IsShuttingDown = false;
        }
    }
}