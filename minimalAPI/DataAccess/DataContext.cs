using Microsoft.EntityFrameworkCore;
using minimalAPI.Models;

namespace minimalAPI.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id= 1,
                    Name = "Shiyang",
                    Email = "Syljob@gmail.com",
                    Address = "19 kensington ve"

                },

                new User
                {
                    Id = 2,
                    Name = "Charlie",
                    Email = "Charlie@gmail.com",
                    Address = "19 kensington Ave"
                }
                );
        }
    }
}
