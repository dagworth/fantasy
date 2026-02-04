public class Location : IMemorable {
    public int id;
    public List<Person> people;

    public Location() {
        people = [];
        id = IdHandler.MakeId(this);
    }
}