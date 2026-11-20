using Microsoft.EntityFrameworkCore;
// All databasåtkomst samlas här. Då slipper menyn och affärslogiken känna till
// EF Core, och alla frågor finns på ett ställe.

public class HotelRepository
{
    private readonly HotelContext _context;

    public HotelRepository(HotelContext context)
    {
        _context = context;
    }

    // Visar alla rum med typ och pris. Include hämtar rumstypen i samma fråga.
    public Task<List<Room>> GetAllRoomsAsync() =>
        _context.Rooms
            .AsNoTracking()
            .Include(r => r.RoomType)
            .OrderBy(r => r.RoomNumber)
            .ToListAsync();
    
    public Task<Room?> GetRoomByNumberAsync(int roomNumber) =>
        _context.Rooms
            .Include(r => r.RoomType)
            .FirstOrDefaultAsync(r => r.RoomNumber == roomNumber);
    
    // E-post görs till gemener så att samma person inte sparas två gånger
// (e-post är unik i databasen). Finns gästen redan återanvänds den.

    public async Task<Guest> GetOrCreateGuestAsync(string name, string email)
    {
       
        var normalized = email.Trim().ToLower();
        var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Email == normalized);

        if (guest == null)
        {
            guest = new Guest { Name = name.Trim(), Email = normalized };
            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();
        }
        
        return guest;
    }
    
    public async Task AddBookingAsync(Booking booking)
    {
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
    }

// "Aktuella" = bokningar som inte redan är avslutade, därför CheckOut > today.
// Include behövs för att visa gästens namn och rumstyp i listan.

    public Task<List<Booking>> GetCurrentBookingsAsync(DateOnly today) =>
        _context.Bookings
            .AsNoTracking()
            .Include(b => b.Guest)
            .Include(b => b.Room).ThenInclude(r => r.RoomType)
            .Where(b => b.CheckOut > today)
            .OrderBy(b => b.CheckIn)
            .ToListAsync();
    
    // Avbokning raderar bokningen helt, så vi inte sparar personuppgifter i onödan.
    public async Task<bool> CancelBookingAsync(int bookingId)
    {
        var booking = await _context.Bookings.FindAsync(bookingId);
        if (booking == null)
            return false;

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();
        return true;
    }

    public Task<List<Treatment>> GetTreatmentsAsync() =>
        _context.Treatments.AsNoTracking().OrderBy(t => t.Id).ToListAsync();

    public async Task AddSpaBookingAsync(SpaBooking spaBooking)
    {
        _context.SpaBookings.Add(spaBooking);
        await _context.SaveChangesAsync();
    }
}