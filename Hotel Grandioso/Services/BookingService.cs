// Ett misslyckat försök (t.ex. fel datum) är ett normalt utfall, inte ett programfel,
// därför returnerar vi ett resultat istället för att kasta ett exception.
public record BookingResult(bool Success, string Message, decimal TotalPrice = 0);

public class BookingService
{
    private readonly HotelRepository _repository;

    public BookingService(HotelRepository repository)
    {
        _repository = repository;
    }

    // Priset räknas här (inte i menyn) så att regeln finns på ett ställe.
    public decimal CalculateTotalPrice(Room room, DateOnly checkIn, DateOnly checkOut)
    {
        int nights = checkOut.DayNumber - checkIn.DayNumber;
        return nights * room.PricePerNight;
    }
    
    public async Task<BookingResult> BookRoomAsync(
        string guestName, string email, int roomNumber, DateOnly checkIn, DateOnly checkOut)
    {
        if (string.IsNullOrWhiteSpace(guestName) || string.IsNullOrWhiteSpace(email))
            return new BookingResult(false, "Namn och e-post måste anges.");

        if (checkOut <= checkIn)
            return new BookingResult(false, "Utcheckning måste vara efter incheckning.");

        if (checkIn < DateOnly.FromDateTime(DateTime.Today))
            return new BookingResult(false, "Incheckning kan inte vara i det förflutna.");

        var room = await _repository.GetRoomByNumberAsync(roomNumber);
        if (room == null)
            return new BookingResult(false, $"Rum {roomNumber} finns inte.");

        var guest = await _repository.GetOrCreateGuestAsync(guestName, email);

        var booking = new Booking
        {
            GuestId = guest.Id,
            RoomId = room.Id,
            CheckIn = checkIn,
            CheckOut = checkOut
        };
        await _repository.AddBookingAsync(booking);

        var total = CalculateTotalPrice(room, checkIn, checkOut);
        return new BookingResult(true, "Bokning bekräftad!", total);
    }
}