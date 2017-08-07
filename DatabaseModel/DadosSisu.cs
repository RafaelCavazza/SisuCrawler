using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DatabaseModel
{
    public class DadosSisu : DbContext
    {
        public DbSet<Aprovado> Aprovados { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<GrauTurno> GrausTurnos { get; set; }
        public DbSet<LocalOferta> LocaisOferta { get; set; }
        public DbSet<Universidade> Universidades { get; set; }
        public DbSet<SolicitacaoDados> SolicitacaoDados { get; set; }

        public DadosSisu(): base()
        {
            var environmentConnectionString = Environment.GetEnvironmentVariable("DadosSisu", EnvironmentVariableTarget.User);
            Database.Connection.ConnectionString = environmentConnectionString;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}