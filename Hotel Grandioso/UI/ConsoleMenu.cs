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
                await ShowRoomsAsync();   // temporary, we replace it with booking soon
            else
                Console.WriteLine("Inte klart än.");
        }
    }

    private async Task ShowRoomsAsync()
    {
        var rooms = await _repository.GetAllRoomsAsync();
        foreach (var r in rooms)
            Console.WriteLine($"  {r.RoomNumber} - {r.RoomType.Name}  {r.SizeM2}m²  {r.PricePerNight} kr/natt");
    }
}