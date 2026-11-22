// Behandlingarna är en meny som många gäster bokar, därför en egen tabell
// så att namn, längd och pris inte upprepas på varje spabokning.
public class Treatment
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public decimal Price { get; set; }

    // En behandling kan bokas många gånger, av olika gäster.
    public ICollection<SpaBooking> SpaBookings { get; set; } = new List<SpaBooking>();
}