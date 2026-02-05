public class Person : IMemorable {
    private string[] logs = [];

    public int id;

    public string name;
    public Races race;
    public int age;
    public Dictionary<int,Memory> memories = [];
    public List<Modifier> modifiers;
    public Stuff[] inventory = [];

    public ILocation location;

    public Personality personality;
    public Stats stats;

    public Perception self_perception;

    public Person(string name, Races race, ILocation loc, Personality p, Stats s, Perception per, int age = 0) {
        this.name = name;
        this.race = race;
        this.age = age;
        self_perception = per;
        location = loc;
        personality = p;
        stats = s;
        modifiers = [];

        id = IdHandler.MakeId(this);
    }
}