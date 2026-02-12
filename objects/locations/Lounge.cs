public class Lounge : ILocation {
    public int index { get; set; }
    public List<int> people { get; set; } = [];
    public List<IEvent> events { get; set; } = [];
    public string locationType { get; set; } = "Lounge";

    public Lounge(int id) {
        index = id;
    }
}