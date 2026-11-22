public class Booking
{
    public int Id { get; set; }

    public int GuestId { get; set; }
    public Guest Guest { get; set; } = null!;

    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;

    // DateOnly eftersom en hotellvistelse gäller hela dagar, inte klockslag.
    public DateOnly CheckIn { get; set; }
    public DateOnly CheckOut { get; set; }
}