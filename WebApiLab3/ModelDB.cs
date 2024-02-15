using Microsoft.EntityFrameworkCore;

namespace WebApiLab3
{
    public class ModelDB:DbContext
    {
        public ModelDB(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Tariff> Tariff { get; set; }
        public DbSet<Parcel> Parcel { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tariff>().HasData(
                new Tariff {Id=1, DepartureCode = "2020", DepartureName = "New York", PricePerWeightUnit = 300 },
                new Tariff { Id = 2, DepartureCode = "0101", DepartureName = "Los Angeles", PricePerWeightUnit = 250 },
                new Tariff { Id = 3, DepartureCode = "4021", DepartureName = "Chicago", PricePerWeightUnit = 320 },
                new Tariff { Id = 4, DepartureCode = "1234", DepartureName = "Houston", PricePerWeightUnit = 280 },
                new Tariff { Id = 5, DepartureCode = "5555", DepartureName = "Miami", PricePerWeightUnit = 270 },
                new Tariff { Id = 6, DepartureCode = "0707", DepartureName = "Seattle", PricePerWeightUnit = 290 }
                );
            modelBuilder.Entity<Parcel>().HasData(
                new Parcel {Id = 1, SenderFullName = "John Doe", DepartureCode = "2020", DepartureName = "New York", Weight = 1.5m, Destination = "Los Angeles", Cost = 50 },
                new Parcel {Id = 2, SenderFullName = "Alice Smith", DepartureCode = "0101", DepartureName = "Los Angeles", Weight = 2.3m, Destination = "Chicago", Cost = 65 },
                new Parcel {Id = 3, SenderFullName = "Bob Johnson", DepartureCode = "4021", DepartureName = "Chicago", Weight = 1.8m, Destination = "Houston", Cost = 55 },
                new Parcel {Id = 4, SenderFullName = "Emily Brown", DepartureCode = "1234", DepartureName = "Houston", Weight = 3.1m, Destination = "Miami", Cost = 75 },
                new Parcel {Id = 5, SenderFullName = "Michael Wilson", DepartureCode = "5555", DepartureName = "Miami", Weight = 2.0m, Destination = "Seattle", Cost = 60 },
                new Parcel {Id = 6, SenderFullName = "Samantha Taylor", DepartureCode = "0707", DepartureName = "Seattle", Weight = 2.5m, Destination = "New York", Cost = 70 }
                );
            modelBuilder.Entity<User>().HasData(
                new User {Id=1, Email = "john@example.com", Password = "password123" },
                new User {Id=2, Email = "alice@example.com", Password = "qwerty456" },
                new User {Id=3, Email = "bob@example.com", Password = "abc123" },
                new User {Id=4, Email = "emma@example.com", Password = "p@ssw0rd" },
                new User {Id=5, Email = "michael@example.com", Password = "securepass" }
                );
        }
    }
}
