public interface ILocation : IMemorable {
    List<int> people { get; set; }
    List<IEvent> events { get; set; }
    string locationType { get; set; }
}