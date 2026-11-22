// Pris och storlek ligger på rummet och inte på rumstypen, eftersom
// två Standard-rum på olika våningar har olika storlek och pris.
public class Room
{
    public int Id { get; set; }
    public int RoomNumber { get; set; }
    public int Floor { get; set; }
    public int SizeM2 { get; set; }
    public decimal PricePerNight { get; set; }

    // Främmande nyckel och navigeringsegenskap: varje rum har exakt en rumstyp.
    public int RoomTypeId { get; set; }
    public RoomType RoomType { get; set; } = null!;

    // Ett rum kan bokas många gånger över tid. Listan behövs för att
    // kunna kontrollera om rummet är upptaget.
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}