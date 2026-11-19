using Microsoft.EntityFrameworkCore;

public class HotelContext : DbContext
{
    public DbSet<RoomType> RoomTypes => Set<RoomType>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Guest> Guests => Set<Guest>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Treatment> Treatments => Set<Treatment>();
    public DbSet<SpaBooking> SpaBookings => Set<SpaBooking>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=grandioso.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>().HasIndex(r => r.RoomNumber).IsUnique();
        modelBuilder.Entity<Guest>().HasIndex(g => g.Email).IsUnique();
        
        modelBuilder.Entity<RoomType>().HasData(
                new RoomType { Id = 1, Name = "Standard", Description = "Ett standardrum" },
                new RoomType { Id = 2, Name = "Luxury", Description = "Ett lyxrum" },
                new RoomType { Id = 3, Name = "Bridal", Description = "Ett bröllopsrum" }
            
          
        );

        modelBuilder.Entity<Room>().HasData(
            new Room { Id = 101, RoomNumber = 101, Floor = 1, SizeM2 = 80, PricePerNight = 800, RoomTypeId = 1 },
            new Room { Id = 102, RoomNumber = 102, Floor = 1, SizeM2 = 80, PricePerNight = 800, RoomTypeId = 1 },
            new Room { Id = 103, RoomNumber = 103, Floor = 1, SizeM2 = 80, PricePerNight = 800, RoomTypeId = 1 },
            new Room { Id = 104, RoomNumber = 104, Floor = 1, SizeM2 = 80, PricePerNight = 800, RoomTypeId = 1 },
            new Room { Id = 105, RoomNumber = 105, Floor = 1, SizeM2 = 80, PricePerNight = 800, RoomTypeId = 1 },

            new Room { Id = 201, RoomNumber = 201, Floor = 2, SizeM2 = 100, PricePerNight = 1000, RoomTypeId = 1 },
            new Room { Id = 202, RoomNumber = 202, Floor = 2, SizeM2 = 100, PricePerNight = 1000, RoomTypeId = 1 },
            new Room { Id = 203, RoomNumber = 203, Floor = 2, SizeM2 = 100, PricePerNight = 1000, RoomTypeId = 1 },
            new Room { Id = 204, RoomNumber = 204, Floor = 2, SizeM2 = 100, PricePerNight = 1000, RoomTypeId = 1 },
            new Room { Id = 205, RoomNumber = 205, Floor = 2, SizeM2 = 100, PricePerNight = 1000, RoomTypeId = 1 },

            new Room { Id = 301, RoomNumber = 301, Floor = 3, SizeM2 = 120, PricePerNight = 1200, RoomTypeId = 1 },
            new Room { Id = 302, RoomNumber = 302, Floor = 3, SizeM2 = 120, PricePerNight = 1200, RoomTypeId = 1 },
            new Room { Id = 303, RoomNumber = 303, Floor = 3, SizeM2 = 120, PricePerNight = 1200, RoomTypeId = 1 },

            new Room { Id = 304, RoomNumber = 304, Floor = 3, SizeM2 = 180, PricePerNight = 2800, RoomTypeId = 2 },
            new Room { Id = 305, RoomNumber = 305, Floor = 3, SizeM2 = 220, PricePerNight = 3500, RoomTypeId = 3 }

        );
        
        modelBuilder.Entity<Treatment>().HasData(
                new Treatment { Id = 1, Name = "Entré (per timme)", DurationMinutes = 60, Price = 150 },
                new Treatment { Id = 2, Name = "Massage (60 min)", DurationMinutes = 60, Price = 950 },
                new Treatment { Id = 3, Name = "Pedikyr (Stortån)", DurationMinutes = 60, Price = 350 },
                new Treatment { Id = 4, Name = "Pedikyr (helfot)", DurationMinutes = 60, Price = 650 }
                
        );
    }
} 