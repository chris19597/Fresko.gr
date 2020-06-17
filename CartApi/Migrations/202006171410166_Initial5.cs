using System.Data.Entity.Migrations;

namespace CartApi.Migrations
{
    public partial class Initial5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Roles",
                    c => new
                    {
                        ID = c.String(false, 128),
                        username = c.String(),
                        role = c.String()
                    })
                .PrimaryKey(t => t.ID);

            DropTable("dbo.Users");
        }

        public override void Down()
        {
            CreateTable(
                    "dbo.Users",
                    c => new
                    {
                        ID = c.String(false, 128),
                        username = c.String(),
                        password = c.String()
                    })
                .PrimaryKey(t => t.ID);

            DropTable("dbo.Roles");
        }
    }
}