using System.Data.Entity.Migrations;

namespace CartApi.Migrations
{
    public partial class Initial4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Cards",
                    c => new
                    {
                        ID = c.String(false, 128),
                        name1 = c.String(),
                        quantity1 = c.String(),
                        name2 = c.String(),
                        quantity2 = c.String(),
                        name3 = c.String(),
                        quantity3 = c.String()
                    })
                .PrimaryKey(t => t.ID);
        }

        public override void Down()
        {
            DropTable("dbo.Cards");
        }
    }
}