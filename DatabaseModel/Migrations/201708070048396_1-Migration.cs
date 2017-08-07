namespace DatabaseModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aprovado",
                c => new
                    {
                        AprovadoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Nota = c.Double(nullable: false),
                        Inscricao = c.String(),
                        Classificacao = c.Int(nullable: false),
                        TipoConcorrencia = c.String(),
                        GrauTurnoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AprovadoId)
                .ForeignKey("dbo.GrauTurno", t => t.GrauTurnoId, cascadeDelete: true)
                .Index(t => t.GrauTurnoId);
            
            CreateTable(
                "dbo.GrauTurno",
                c => new
                    {
                        GrauTurnoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        CodigoSisu = c.String(),
                        CursoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GrauTurnoId)
                .ForeignKey("dbo.Curso", t => t.CursoId, cascadeDelete: true)
                .Index(t => t.CursoId);
            
            CreateTable(
                "dbo.Curso",
                c => new
                    {
                        CursoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        CodigoSisu = c.String(),
                        LocalOfertaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CursoId)
                .ForeignKey("dbo.LocalOferta", t => t.LocalOfertaId, cascadeDelete: true)
                .Index(t => t.LocalOfertaId);
            
            CreateTable(
                "dbo.LocalOferta",
                c => new
                    {
                        LocalOfertaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        CodigoSisu = c.String(),
                        UniversidadeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LocalOfertaId)
                .ForeignKey("dbo.Universidade", t => t.UniversidadeId, cascadeDelete: true)
                .Index(t => t.UniversidadeId);
            
            CreateTable(
                "dbo.Universidade",
                c => new
                    {
                        UniversidadeId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.UniversidadeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LocalOferta", "UniversidadeId", "dbo.Universidade");
            DropForeignKey("dbo.Curso", "LocalOfertaId", "dbo.LocalOferta");
            DropForeignKey("dbo.GrauTurno", "CursoId", "dbo.Curso");
            DropForeignKey("dbo.Aprovado", "GrauTurnoId", "dbo.GrauTurno");
            DropIndex("dbo.LocalOferta", new[] { "UniversidadeId" });
            DropIndex("dbo.Curso", new[] { "LocalOfertaId" });
            DropIndex("dbo.GrauTurno", new[] { "CursoId" });
            DropIndex("dbo.Aprovado", new[] { "GrauTurnoId" });
            DropTable("dbo.Universidade");
            DropTable("dbo.LocalOferta");
            DropTable("dbo.Curso");
            DropTable("dbo.GrauTurno");
            DropTable("dbo.Aprovado");
        }
    }
}
