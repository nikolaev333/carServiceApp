using CarServiceApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarServiceApp.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceEvent> ServiceEvents { get; set; }

        // Презаписване на метода OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

     
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Owner)
                .WithMany(u => u.Cars)
                .HasForeignKey(c => c.OwnerUserId);

            modelBuilder.Entity<User>()
      .Property(u => u.Role)
      .HasConversion<string>(); // Съхранява ролите като string в базата данни

            modelBuilder.Entity<Service>()
                .HasOne(s => s.Car)
                .WithMany(c => c.Services)
                .HasForeignKey(s => s.CarId);

            modelBuilder.Entity<ServiceEvent>()
                .HasOne(se => se.Service)
                .WithMany(s => s.ServiceEvents)
                .HasForeignKey(se => se.ServiceId);


        }
    }
}
