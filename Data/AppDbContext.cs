using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        private string Path { get; set; }
        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<OrganizationEntity> Organizations { get; set; }
        public DbSet<CarEntity> Cars { get; set; }
        public AppDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            Path = System.IO.Path.Join(path, "contacts.db");

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data source={Path}");
            //optionsBuilder.EnableSensitiveDataLogging();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var admin = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "adam",
                NormalizedUserName = "ADAM",
                Email = "adam@wsei.edu.pl",
                NormalizedEmail = "ADAM@WSEI.EDU.PL",
                EmailConfirmed = true,

            };

            var user = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Jakub",
                NormalizedUserName = "JAKUB",
                Email = "jakubwasylik@gmail.com",
                NormalizedEmail = "JAKUBWASYLIK@GMAIL.COM",
                EmailConfirmed = true,
            };
            PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();

            admin.PasswordHash = passwordHasher.HashPassword(admin, "1234Aaaa!");
            user.PasswordHash = passwordHasher.HashPassword(user, "1234Bbbb!");

            modelBuilder.Entity<IdentityUser>().HasData(admin);
            modelBuilder.Entity<IdentityUser>().HasData(user);

            // tworzenie roli admina
            var adminRole = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "admin",
                NormalizedName = "ADMIN"
            };

            adminRole.ConcurrencyStamp = adminRole.Id;
            modelBuilder.Entity<IdentityRole>()
                .HasData(adminRole);

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = adminRole.Id,
                    UserId = admin.Id
                }
            );

            modelBuilder.Entity<ContactEntity>()
               .HasOne(c => c.Organization)
               .WithMany(o => o.Contacts)
               .HasForeignKey(c => c.OrganizationId);

            modelBuilder.Entity<OrganizationEntity>()
                .HasData(
                new OrganizationEntity()
                {
                    Id = 101,
                    Name = "WSEI",
                    Description = "Uczelnia wyższa",

                },
                new OrganizationEntity()
                {
                    Id = 102,
                    Name = "Comarch",
                    Description = "Przedsiębiorstwo IT",
                }
                );
            modelBuilder.Entity<ContactEntity>()
                .HasData(
                new ContactEntity()
                {
                    ContactId = 1,
                    Name = "Kuba",
                    Email = "kuba@gmail.com",
                    Phone = "748657291",
                    Birth = DateTime.Parse("2002-09-10"),
                    OrganizationId = 101
                },
                new ContactEntity()
                {
                    ContactId = 2,
                    Name = "Paweł",
                    Email = "pawel@gmail.com",
                    Phone = "758464718",
                    Birth = DateTime.Parse("2001-01-12"),
                    OrganizationId = 102
                }
                );

            modelBuilder.Entity<OrganizationEntity>()
                .OwnsOne(o => o.Address)
                .HasData(
                new
                {
                    OrganizationEntityId = 101,
                    City = "Kraków",
                    Street = "św. Filipa 17",
                    PostalCode = "31-150"
                },
                new
                {
                    OrganizationEntityId = 102,
                    City = "Kraków",
                    Street = "Gronowa 20",
                    PostalCode = "30-380"
                }
                );

            modelBuilder.Entity<CarEntity>()
                .HasData(
                new CarEntity()
                {
                    CarId = 1,
                    Model = "BMW X3 F25 SUV 2014",
                    Manufacturer = "BMW",
                    EngineCapacity = 2.0,
                    Power = 150,
                    EngineType = "Diesel",
                    RegistrationNumber = "KR R239J",
                    Owner = "Jakub Romaniuk"
                },
                new CarEntity()
                {
                    CarId = 2,
                    Model = "Audi A3 (8V 2016)",
                    Manufacturer = "AUDI AG",
                    EngineCapacity = 1.6,
                    Power = 110,
                    EngineType = "Diesel",
                    RegistrationNumber = "KR 25K2M",
                    Owner = "Marcin Pawlacz"
                },
                new CarEntity()
                {
                    CarId = 3,
                    Model = "Volkswagen Arteon Fastback 2018",
                    Manufacturer = "Volkswagen Group",
                    EngineCapacity = 2.0,
                    Power = 280,
                    EngineType = "Petrol engine",
                    RegistrationNumber = "KR R56LK",
                    Owner = "Mariusz Pogonowski"
                }
                );
        }
    }
}
