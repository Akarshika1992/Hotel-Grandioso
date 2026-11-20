// Menyn ansvarar bara för in- och utmatning. Logik och databas ligger i andra klasser.
public class ConsoleMenu
{
    private readonly HotelRepository _repository;
    private readonly BookingService _service;

    public ConsoleMenu(HotelRepository repository, BookingService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== Hotel Grandioso ===");
            Console.WriteLine();
            Console.WriteLine("1. Boka rum");
            Console.WriteLine("2. Avboka");
            Console.WriteLine("3. Visa bokningar");
            Console.WriteLine("4. Visa lediga rum");
            Console.WriteLine("5. Boka spa");
            Console.WriteLine("6. Avsluta");
            Console.WriteLine();
            Console.Write("Val: ");

            var choice = Console.ReadLine();
            
            if (choice == "6")
                return;

            if (choice == "1")
                await BookRoomAsync();
            else if (choice == "2")
                await CancelAsync();
            else if (choice == "3")
                await ShowBookingsAsync();
            else if (choice == "4")
                await ShowRoomsAsync();
            else
                Console.WriteLine("Inte klart än.");
        }
    }

    private async Task BookRoomAsync()
    {
        Console.Write("Gästnamn: ");
        var name = Console.ReadLine() ?? "";

        Console.Write("E-post: ");
        var email = Console.ReadLine() ?? "";

        if (!ReadDate("Incheckning (YYYY-MM-DD): ", out var checkIn)) return;
        if (!ReadDate("Utcheckning (YYYY-MM-DD): ", out var checkOut)) return;

        Console.WriteLine("Välj rum:");
        await ShowRoomsAsync();

        Console.Write("Rumsnummer: ");
        if (!int.TryParse(Console.ReadLine(), out var roomNumber))
        {
            Console.WriteLine("Ogiltigt rumsnummer.");
            return;
        }

        var result = await _service.BookRoomAsync(name, email, roomNumber, checkIn, checkOut);

        if (result.Success)
            Console.WriteLine($"{result.Message} Totalt: {result.TotalPrice} kr");
        else
            Console.WriteLine(result.Message);
    }

    private async Task ShowRoomsAsync()
    {
        var rooms = await _repository.GetAllRoomsAsync();
        foreach (var r in rooms)
            Console.WriteLine($"  {r.RoomNumber} - {r.RoomType.Name}  {r.SizeM2}m²  {r.PricePerNight} kr/natt");
    }

    private async Task ShowBookingsAsync()
    {
        var bookings = await _repository.GetCurrentBookingsAsync(DateOnly.FromDateTime(DateTime.Today));

        if (bookings.Count == 0)
        {
            Console.WriteLine("Inga aktuella bokningar.");
            return;
        }

        foreach (var b in bookings)
        {
            var total = _service.CalculateTotalPrice(b.Room, b.CheckIn, b.CheckOut);
            Console.WriteLine($"  #{b.Id}  {b.Guest.Name}  rum {b.Room.RoomNumber} ({b.Room.RoomType.Name})  {b.CheckIn:yyyy-MM-dd} -> {b.CheckOut:yyyy-MM-dd}  {total} kr");
        }
    }

    private async Task CancelAsync()
    {
        // Visar bokningarna först så att användaren ser vilket id som ska anges.
        await ShowBookingsAsync();

        Console.Write("Boknings-id att avboka: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Ogiltigt id.");
            return;
        }

        bool avbokad = await _repository.CancelBookingAsync(id);

        if (avbokad)
            Console.WriteLine("Bokningen är avbokad.");
        else
            Console.WriteLine("Ingen bokning med det id:t hittades.");
    }
    
    // Strikt format (yyyy-MM-dd) så att datum inte tolkas olika beroende på datorns språk.
    private static bool ReadDate(string prompt, out DateOnly date)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();

        if (DateOnly.TryParseExact(input, "yyyy-MM-dd", out date))
            return true;

        Console.WriteLine("Ogiltigt datum, använd formatet YYYY-MM-DD.");
        return false;
    }
}