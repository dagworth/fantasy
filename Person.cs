public class Person : IMemorable {
    private string[] logs = [];

    public int id;

    public string name;
    public Races race;
    public int age;
    public Dictionary<int,Memory> memories = [];
    public Stuff[] inventory = [];

    public Location location;

    public Personality personality;
    public Stats stats;

    public Person(string name, Races race, Location loc, Personality p, Stats s, int age = 0) {
        this.name = name;
        this.race = race;
        this.age = age;
        location = loc;
        personality = p;
        stats = s;

        id = IdHandler.MakeId(this);
    }
}