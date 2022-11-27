using Microsoft.EntityFrameworkCore;

namespace LetsOrganize.Entities
{
    public class LetsOrganizeDbContext : DbContext
    {
        public DbSet<MyList> Lists { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DbSet<User> Users { get; set; }

        public LetsOrganizeDbContext(DbContextOptions<LetsOrganizeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyList>(l =>
            {
                l.HasData(
                        new MyList()
                        {
                            Id = 1,
                            Name = "ShoppingList"
                        });
            });

            modelBuilder.Entity<Element>(e =>
            {
                e.HasOne(l => l.MyList)
                .WithMany(e => e.ElementsList)
                .HasForeignKey(e => e.MyListId);

                e.HasData(
                     new Element { MyListId = 1, Id = 1, Name = "Apples", Quantity = 1, Unit = "kg" },
                     new Element { MyListId = 1, Id = 2, Name = "Potatoes", Quantity = 3, Unit = "kg" });
            });

        }
    }
}
