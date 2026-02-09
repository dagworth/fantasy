public interface ILocation : IMemorable {
    List<Person> people { get; set; }
    List<IEvent> events { get; set; }
    string locationType { get; set; }
}