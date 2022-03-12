using Alien.Web.Scheduler;
using BusinessLogic;
using DataAccess;
using DNTScheduler.Core;
using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Alien.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            AppConfiguration.MySqlConnectionString = Configuration["ConexaoMySql:MySqlConnectionString"];
            AppConfiguration.ChaveCriptografia = Configuration["ChaveCriptografia"];
            AppConfiguration.VetorInicializacaoCriptografia = Configuration["VetorInicializacaoCriptografia"];

            services.AddDbContext<ConexaoBanco>(options => options.UseMySql(AppConfiguration.MySqlConnectionString, ServerVersion.AutoDetect(AppConfiguration.MySqlConnectionString)).UseLazyLoadingProxies());

            //services.AddTransient<IBaseRepository, BaseRepository>();

            //services.AddTransient<IBaseServices<T>, BaseServices>();
            services.AddTransient<IBaseServices<AlienDB_Empresa>, BaseServices<AlienDB_Empresa>>();
            services.AddTransient<IBaseServices<AlienDB_Imovel>, BaseServices<AlienDB_Imovel>>();
            services.AddTransient<IBaseServices<AlienDB_Midia>, BaseServices<AlienDB_Midia>>();
            services.AddTransient<IBaseServices<AlienDB_Tipo_Imovel>, BaseServices<AlienDB_Tipo_Imovel>>();
            services.AddTransient<IBaseServices<AlienDB_Usuario_Sistema>, BaseServices<AlienDB_Usuario_Sistema>>();
            services.AddTransient<IBaseServices<AlienDB_Cadastre_Seu_Imovel>, BaseServices<AlienDB_Cadastre_Seu_Imovel>>();
            services.AddTransient<IBaseServices<AlienDB_Empreendimentos>, BaseServices<AlienDB_Empreendimentos>>();
            services.AddTransient<IBaseServices<AlienDB_Regiao>, BaseServices<AlienDB_Regiao>>();

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews();

            services.AddDNTScheduler(options =>
            {
                // DNTScheduler needs a ping service to keep it alive.
                // If you don't need it, don't add it!
                options.AddPingTask(siteRootUrl: "https://localhost:44393");

                options.AddScheduledTask<DoUpdateDefaultImage>(
                    runAt: utcNow =>
                    {
                        var now = utcNow;//.AddMinutes(30);
                        return (now.Minute == 0 || (now.Minute % 10 == 0)); //Executa a cada 10 minutos
                    },
                    order: 2);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Definindo a cultura padrão: pt-BR
            var supportedCultures = new[] { new CultureInfo("en-US") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseStatusCodePages();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=IndexModal}");
                endpoints.MapControllers();
            });

            app.UseDNTScheduler();
        }
    }
}
