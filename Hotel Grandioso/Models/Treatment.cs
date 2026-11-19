
public class Treatment
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public decimal Price { get; set; } 
    

    // One Treatment can be booked many times ( by Different Guests)
  
    public ICollection<SpaBooking> SpaBookings { get; set; } = new List<SpaBooking>();
}