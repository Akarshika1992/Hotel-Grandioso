public class Room
{
    public int Id { get; set; }
    public int RoomNumber { get; set; }
    public int Floor { get; set; }
    public int SizeM2 { get; set; }
    public decimal PricePerNight { get; set; }

    // Foreign key and navigation property: each room has exactly one type
    public int RoomTypeId { get; set; }
    public RoomType RoomType { get; set; } = null!;

    // One room can be booked many times over time
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}