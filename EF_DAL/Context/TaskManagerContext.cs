using EF_DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_DAL.Context
{
    public class TaskManagerContext : DbContext
    {
        public DbSet<Team> Team { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Grindstone> Grindstone { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<GrindstoneEmployee> GrindstoneEmployee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=taskmanager;user=root;password=");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired();

            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.FirstName).IsRequired();
                entity.HasOne<Team>(e => e.Team)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.TeamId);

            });


            modelBuilder.Entity<Grindstone>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.Priority).IsRequired();
                entity.Property(e => e.StatusId).IsRequired();
                entity.HasOne<Status>(e => e.Status)
                .WithMany(e => e.Grindstones)
                .HasForeignKey(e => e.StatusId);
            });


            modelBuilder.Entity<GrindstoneEmployee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne<Employee>(e => e.Employee)
                .WithMany(e => e.Assignments)
                .HasForeignKey(e => e.EmployeeId);
                entity.HasOne<Grindstone>(e => e.Grindstone)
                .WithMany(e => e.Assignments)
                .HasForeignKey(e => e.GrindstoneId);
            });
                        
        }
    }
}
