namespace DatabaseModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SolicitacaoDados",
                c => new
                    {
                        SolicitacaoDadosId = c.Int(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SolicitacaoDadosId);
            
            AddColumn("dbo.Universidade", "SolicitacaoDadosId", c => c.Int(nullable: false));
            AlterColumn("dbo.Aprovado", "Nota", c => c.String());
            CreateIndex("dbo.Universidade", "SolicitacaoDadosId");
            AddForeignKey("dbo.Universidade", "SolicitacaoDadosId", "dbo.SolicitacaoDados", "SolicitacaoDadosId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Universidade", "SolicitacaoDadosId", "dbo.SolicitacaoDados");
            DropIndex("dbo.Universidade", new[] { "SolicitacaoDadosId" });
            AlterColumn("dbo.Aprovado", "Nota", c => c.Double(nullable: false));
            DropColumn("dbo.Universidade", "SolicitacaoDadosId");
            DropTable("dbo.SolicitacaoDados");
        }
    }
}
