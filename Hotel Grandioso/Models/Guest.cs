// Medvetet bara namn och e-post: hårfärg, längd och religion behövs inte
// för en bokning och sparas därför inte (dataminimering, se README).
public class Guest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // E-post används för bekräftelse och för att skilja gäster med samma namn åt.
    public string Email { get; set; } = string.Empty;

    // En gäst kan göra många rumsbokningar och många spabokningar.
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<SpaBooking> SpaBookings { get; set; } = new List<SpaBooking>();
}