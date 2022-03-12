using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ConexaoBanco : DbContext
    {
        public ConexaoBanco(DbContextOptions<ConexaoBanco> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                var connectionString = AppConfiguration.MySqlConnectionString;// AppConfiguration.GetConnectionString();

                dbContextOptionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(AppConfiguration.MySqlConnectionString))//;
                                       .UseLazyLoadingProxies();//Configuration.GetConnectionString("ConexaoMySql:MySqlConnectionString"));
            }
        }

        public DbSet<AlienDB_Empresa> AlienDB_Empresa { get; set; }

        public DbSet<AlienDB_Imovel> AlienDB_Imovel { get; set; }

        public DbSet<AlienDB_Midia> AlienDB_Midia { get; set; }

        public DbSet<AlienDB_Tipo_Imovel> AlienDB_Tipo_Imovel { get; set; }

        public DbSet<AlienDB_Usuario_Sistema> AlienDB_Usuario_Sistema { get; set; }

        public DbSet<AlienDB_Cadastre_Seu_Imovel> AlienDB_Cadastre_Seu_Imovel { get; set; }

        public DbSet<AlienDB_Empreendimentos> AlienDB_Empreendimentos { get; set; }

        public DbSet<AlienDB_Regiao> AlienDB_Regiao { get; set; }

        public void Save()
        {
            base.SaveChanges();
        }
    }
}