using System.Data.Entity.Migrations;

namespace CartApi.Migrations
{
    public partial class Initial9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Cardhistories",
                    c => new
                    {
                        ID = c.String(false, 128),
                        username = c.String(),
                        CardID = c.String()
                    })
                .PrimaryKey(t => t.ID);
        }

        public override void Down()
        {
            DropTable("dbo.Cardhistories");
        }
    }
}