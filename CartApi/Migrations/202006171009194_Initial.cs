using System.Data.Entity.Migrations;

namespace CartApi.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Proions",
                    c => new
                    {
                        ID = c.String(false, 128),
                        Name = c.String(),
                        Description = c.String(),
                        Qantity = c.String(),
                        Price = c.String()
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                    "dbo.Users",
                    c => new
                    {
                        ID = c.String(false, 128),
                        username = c.String(),
                        password = c.String()
                    })
                .PrimaryKey(t => t.ID);
        }

        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Proions");
        }
    }
}