using System.Data.Entity;
using CartApi.Models;

namespace CartApi
{
    public class Context : DbContext
    {
        public Context() : base("DefaultConnection")
        {
        }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Proion> Proioda { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Cardhistory> CardsH { get; set; }
    }
}