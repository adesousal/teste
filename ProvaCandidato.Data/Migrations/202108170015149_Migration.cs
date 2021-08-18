namespace ProvaCandidato.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Observacoes",
                c => new
                    {
                        codigo = c.Int(nullable: false, identity: true),
                        observacao = c.String(),
                        Cliente_Codigo = c.Int(),
                    })
                .PrimaryKey(t => t.codigo)
                .ForeignKey("dbo.Cliente", t => t.Cliente_Codigo)
                .Index(t => t.Cliente_Codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Observacoes", "Cliente_Codigo", "dbo.Cliente");
            DropIndex("dbo.Observacoes", new[] { "Cliente_Codigo" });
            DropTable("dbo.Observacoes");
        }
    }
}
