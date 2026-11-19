public class Guest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // One guest can have many room bookings and many spa bookings
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<SpaBooking> SpaBookings { get; set; } = new List<SpaBooking>();
}

// Medvetet bara namn och e-post: hårfärg, längd och religion behövs inte
// för en bokning och sparas därför inte (dataminimering, se README).