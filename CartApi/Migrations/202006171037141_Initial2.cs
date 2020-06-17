using System.Data.Entity.Migrations;

namespace CartApi.Migrations
{
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Cards",
                    c => new
                    {
                        ID = c.String(false, 128)
                    })
                .PrimaryKey(t => t.ID);

            AddColumn("dbo.Proions", "Card_ID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Proions", "Card_ID");
            AddForeignKey("dbo.Proions", "Card_ID", "dbo.Cards", "ID");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Proions", "Card_ID", "dbo.Cards");
            DropIndex("dbo.Proions", new[] {"Card_ID"});
            DropColumn("dbo.Proions", "Card_ID");
            DropTable("dbo.Cards");
        }
    }
}