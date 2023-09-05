using Microsoft.EntityFrameworkCore;
using DesignBureauWebApplication.Models;
using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DesignBureauWebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser> // , AppRole
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Assignment> Assignment { get; set; }
        public DbSet<AssignmentDictionary> AssignmentDictionary { get; set; }
        public DbSet<AssignmentHistory> AssignmentHistory { get; set; }
        public DbSet<Consumption> Consumption { get; set; }
        //public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<EquipmentDictionary> EquipmentDictionary { get; set; }
        public DbSet<EquipmentHistory> EquipmentHistory { get; set; }
        public DbSet<Execution> Execution { get; set; }
        //public DbSet<Interaction> Interaction { get; set; }
        //public DbSet<InteractionDictionary> InteractionDictionary { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        //public DbSet<InventoryOrder> InventoryOrder { get; set; }
        public DbSet<InventoryTransportation> InventoryTransportation { get; set; }
        public DbSet<InventoryTransportationHistory> InventoryTransportationHistory { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<MaterialDictionary> MaterialDictionary { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderHistory> OrderHistory { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectHistory> ProjectHistory { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Transportation> Transportation { get; set; }
        public DbSet<TransportationHistory> TransportationHistory { get; set; }
        public DbSet<Work> Work { get; set; }
        public DbSet<WorkDictionary> WorkDictionary { get; set; }
        public DbSet<WorkHistory> WorkHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Transportation>()
                .HasOne(t => t.Origin)
                .WithMany()
                .HasForeignKey(t => t.OriginId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transportation>()
                .HasOne(t => t.Destination)
                .WithMany()
                .HasForeignKey(t => t.DestinationId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ExecutionOrder>()
                .HasOne(e => e.Work)
                .WithMany()
                .HasForeignKey(e => e.WorkId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ExecutionOrder>()
                .HasOne(e => e.NextWork)
                .WithMany()
                .HasForeignKey(e => e.NextWorkId)
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Location>()
            //    .HasMany(l => l.Origins)
            //    .WithOne(t => t.Origin)
            //    .HasForeignKey(t => t.OriginId);

            //modelBuilder.Entity<Location>()
            //    .HasMany(l => l.Destinations)
            //    .WithOne(t => t.Destination)
            //    .HasForeignKey(t => t.DestinationId);
            modelBuilder.Ignore<IdentityUserLogin<string>>();
        }
        
    }
}