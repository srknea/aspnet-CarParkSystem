using CarParkSystem.Core.Model;
using CarParkSystem.Core.Model.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Repository
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<FirstClassVehicleFeature> FirstClassVehicleFeatures { get; set; }
        public DbSet<SecondClassVehicleFeature> SecondClassVehicleFeatures { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.Entity<Vehicle>().HasOne(x => x.FirstClassVehicleFeature).WithOne(x => x.Vehicle).HasForeignKey<FirstClassVehicleFeature>(x => x.Id); // Bire-bir ilişkide primary key'i aynı zamanda foreign key olarak ayarlandı.
            modelBuilder.Entity<Vehicle>().HasOne(x => x.SecondClassVehicleFeature).WithOne(x => x.Vehicle).HasForeignKey<SecondClassVehicleFeature>(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
