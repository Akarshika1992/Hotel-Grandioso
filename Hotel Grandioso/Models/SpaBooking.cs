public class SpaBooking
{
    public int Id { get; set; }

    public int GuestId { get; set; }
    public Guest Guest { get; set; } = null!;

    public int TreatmentId { get; set; }
    public Treatment Treatment { get; set; } = null!;
// DateTime eftersom en behandling bokas på en tid på dagen.
    public DateTime Date { get; set; }
   
}