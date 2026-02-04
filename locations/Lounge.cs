public class Lounge : ILocation {
    public int id { get; set; }
    public List<Person> people { get; set; }
    public string locationType { get; set; }

    public Lounge() {
        locationType = "Lounge";
        people = [];
        id = IdHandler.MakeId(this);
    }
}