using System.Collections.Concurrent;

public interface ILocation {
    int index { get; set; }
    List<int> people { get; set; }
    List<IEvent> events { get; set; }
    string locationType { get; set; }
}