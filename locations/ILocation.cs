public interface ILocation : IMemorable {
    int id { get; set; }
    List<Person> people { get; set; }
    string locationType { get; set; }
}