using System.Reflection;
using System.Collections.Concurrent;
using System.Drawing;

public class Population {
    private Simulation sim;

    public Random random;
    public int people_count;

    public List<string> logs;

    public List<string> names;
    public List<Races> races;
    public List<int> ages;
    public List<ILocation> locations;
    public List<Personality> personalities;
    public List<Stats> stats;
    public List<Perception> self_perceptions;

    public List<Memory> memories;
    public List<List<Modifier>> modifiers;

    public Population(Simulation sim, int size = 4000) {
        this.sim = sim;

        logs = new(size);
        names = new(size);
        races = new(size);
        ages = new(size);
        locations = new(size);
        personalities = new(size);
        stats = new(size);
        self_perceptions = new(size);

        memories = new(size);
        modifiers = new(size);

        random = new Random();
    }

    public void Simulate() {
        //do later
    }

    public int createPerson(Races race, string name) {
        int id = people_count++;
        RaceData info = DefaultData.get(race);

        Perception perception = new();
        Personality personality = new();
        Stats stat = new();

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
            property.SetValue(stat, final_val);
            property.SetValue(perception, self_per);
        }

        ILocation loc = sim.map.GetLocation(random.Next(0,sim.map.size));

        names.Add(name);
        races.Add(race);
        locations.Add(loc);
        loc.people.Add(id);
        personalities.Add(personality);
        stats.Add(stat);
        self_perceptions.Add(perception);
        ages.Add(random.Next(0,70) + 30);
        return id;
    }
}