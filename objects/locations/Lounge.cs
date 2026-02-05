public class Lounge : ILocation {
    public int id { get; set; }
    public List<Person> people { get; set; } = [];
    public List<IEvent> events { get; set; } = [];
    public string locationType { get; set; } = "Lounge";

    public Lounge() {
        id = IdHandler.MakeId(this);
    }
}