using System.Reflection;
using System.Collections.Concurrent;
using System.Drawing;

public class Population {
    private Simulation sim;

    public Random random;
    public int people_count;

    public ConcurrentDictionary<int,string> logs;

    public ConcurrentDictionary<int,string> names;
    public ConcurrentDictionary<int,Races> races;
    public ConcurrentDictionary<int,int> ages;
    public ConcurrentDictionary<int,ILocation> locations;
    public ConcurrentDictionary<int,Personality> personalities;
    public ConcurrentDictionary<int,Stats> stats;
    public ConcurrentDictionary<int,Perception> self_perceptions;

    public ConcurrentDictionary<int,ConcurrentDictionary<int,int>> memories;
    public ConcurrentDictionary<int,ConcurrentBag<int>> modifiers;

    public Population(Simulation sim) {
        this.sim = sim;

        logs = [];
        names = [];
        races = [];
        ages = [];
        locations = [];
        personalities = [];
        stats = [];
        self_perceptions = [];

        memories = [];
        modifiers = [];

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
        loc.people.Add(id);

        names[id] = name;
        races[id] = race;
        locations[id] = loc;
        personalities[id] = personality;
        stats[id] = stat;
        memories[id] = [];
        modifiers[id] = [];
        self_perceptions[id] = perception;
        ages[id] = random.Next(0,70) + 30;
        return id;
    }
}