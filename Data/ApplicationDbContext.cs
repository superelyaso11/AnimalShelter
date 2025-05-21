using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using AnimalShelter.Models;

namespace AnimalShelter.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Database tables here
        public DbSet<Animal>? Animals { get; set; }
        public DbSet<AdoptionApplication>? AdoptionApplications { get; set; }

        public DbSet<Donation>? Donations { get; set; }
        
        public DbSet<Contact>? Contact { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Animal entity
            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Breed).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Description).IsRequired().HasMaxLength(500);
                entity.Property(a => a.Image).IsRequired().HasMaxLength(200);
                entity.Property(a => a.IsAdopted).HasDefaultValue(false);
            });

            // Configure AdoptionApplication entity
            modelBuilder.Entity<AdoptionApplication>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.FullName).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Email).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Phone).IsRequired().HasMaxLength(20);
                entity.Property(a => a.Address).IsRequired().HasMaxLength(200);
                entity.Property(a => a.ApplicationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Relationship with Animal
                entity.HasOne(a => a.Animal)
                      .WithMany()
                      .HasForeignKey(a => a.AnimalId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Contact entity
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Message).IsRequired().HasMaxLength(500);
            });
        }
    }
}