using System.Reflection;

public class Simulation {
    private readonly int size;
    private readonly Random random = new Random();
    private readonly List<Person> people = [];
    private readonly ILocation[] map;

    private List<IDecisionStep> decision_steps;

    public Dictionary<Person, Perception> crowd_perceptions = [];

    public Simulation(int x, int y) {
        size = x*y;
        map = new ILocation[size];
        for(int i = 0; i < size; i++) {
            map[i] = new Lounge();
        }

        decision_steps = new List<IDecisionStep> {
            new stage1(this),
            new stage2(this),
            new stage3(this),
            new stage4(this)
        };
    }

    public void Simulate() {
        foreach (IDecisionStep step in decision_steps) {
            foreach (Person a in people) {
                step.execute(a);
            }
        }
    }

    public Person createPerson(Races race, string name) {
        RaceData info = DefaultData.get(race);

        Perception perception = new();
        Personality personality = new();
        Stats stats = new();

        foreach (PropertyInfo property in typeof(IPersonality).GetProperties()) {
            double base_val = (double)property.GetValue(info.personality)!;
            double multiplier = .7 + (random.NextDouble() * .6);
            double final_val = (double)Math.Round(base_val * multiplier);
            double self_per = (double)Math.Round(final_val * multiplier);
            property.SetValue(personality, final_val);
            property.SetValue(perception, self_per);
        }

        foreach (PropertyInfo property in typeof(IStats).GetProperties()) {
            double base_val = (double)property.GetValue(info.stats)!;
            double multiplier = .7 + (random.NextDouble() * .6);
            double final_val = (double)Math.Round(base_val * multiplier);
            double self_per = (double)Math.Round(final_val * multiplier);
            property.SetValue(stats, final_val);
            property.SetValue(perception, self_per);
        }

        ILocation loc = map[random.Next(0,size)];
        Person clone = new(name, race, loc, personality, stats, perception, random.Next(0,70) + 30);
        loc.people.Add(clone);

        people.Add(clone);

        return clone;
    }
}