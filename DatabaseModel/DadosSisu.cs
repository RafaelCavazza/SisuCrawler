namespace DatabaseModel
{
    using System.Data.Entity;

    public class DadosSisu : DbContext
    {
        // Your context has been configured to use a 'DadosSisu' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DatabaseModel.DadosSisu' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DadosSisu' 
        // connection string in the application configuration file.

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Aluno> Cursos { get; set; }
        public DbSet<GrauTurno> GrausTurnos { get; set; }
        public DbSet<LocalOferta> LocaisOferta { get; set; }
        public DbSet<Universidade> Universidades { get; set; }

        public DadosSisu()
            : base("name=DadosSisu")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }
}