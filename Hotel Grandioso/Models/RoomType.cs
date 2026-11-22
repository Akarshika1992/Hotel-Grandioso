// Rumstypen är en egen tabell så att namn och beskrivning bara lagras en gång
// istället för att upprepas på varje rum.
public class RoomType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // En rumstyp kan finnas på många rum.
    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}