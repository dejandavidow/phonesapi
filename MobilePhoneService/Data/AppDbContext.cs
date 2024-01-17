using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MobilePhoneService.Models;

namespace MobilePhoneService.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Phone> Phones { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Manufacturer>().HasData(
                new Manufacturer() { Id = 1, Name = "Xiaomi", Country = "Kina" },
                new Manufacturer() { Id = 2, Name = "Apple", Country = "SAD" },
                new Manufacturer() { Id = 3, Name = "Huawei", Country = "Kina" }
                );
            builder.Entity<Phone>().HasData(
                new Phone() { Id = 1, Model="A94", OperatingSystem = "Android", Size=12, Price = 31125.42m, ManufacturerId = 3 },
                new Phone() { Id = 2, Model="13T Pro", OperatingSystem = "Android", Size=7, Price = 104999.99m, ManufacturerId = 1 },
                new Phone() { Id = 3, Model="11", OperatingSystem = "iOS", Size=17, Price = 71290.35m, ManufacturerId = 2 },
                new Phone() { Id = 4, Model="Reno10 Pro", OperatingSystem = "Android", Size=4, Price = 68264.74m, ManufacturerId = 3 },
                new Phone() { Id = 5, Model="12 Lite", OperatingSystem = "Android", Size=5, Price = 44999.56m, ManufacturerId = 1 }
                );
            base.OnModelCreating(builder);
        }
    }
}
