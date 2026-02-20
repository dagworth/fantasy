public class Stealing : IEvent {
    public int id { get; set; }
    private Random rng = new();

    public int discreteness {get; set;}
    public int visibility {get; set;}
    public int[] participants {get; set;} = [];

    //there will be a list of all actions that are possible after witnessing an event
    //this is so we can like change what the person thinks after the event
    //not sure if we should keep this cus sometimes the person wont care so ignored isnt the best way to go about it

    private List<int> shout = []; //person that will shout out that theres a thief
    private List<int> heroes = []; //people that will try to help the victim
    private List<int> ignored = []; //people that will do nothing

    public void SetUp(Simulation sim, int[] participants) {
        this.participants = participants;
        discreteness = 85;
        visibility = 0;

        Console.WriteLine($"{participants[0]} trying to steal from {participants[1]}");
    }

    //stealing_index should be cached somewhere
    public IEvent? choose_if_event(Simulation sim, int guy) {
        Personality s = sim.people.personalities[guy];
        double stealing_index = 0
            + rng.NextDouble() * MathHelper.GetWeight2(s.jealousy)
            - rng.NextDouble() * MathHelper.GetWeight3(s.moral)
            - rng.NextDouble() * MathHelper.GetWeight2(s.prideful)
            + rng.NextDouble() * MathHelper.GetWeight2(s.risky)
            + rng.NextDouble() * MathHelper.GetWeight2(s.confident);

        if (stealing_index < 9500) return null; //temp to help with performance

        var people = sim.people.locations[guy].people;

        for(int i = 0; i < people.Count; i++) {
            int o = people[i];
            if(o == guy) continue;

            Stats stats = sim.people.stats[o];
            
            double want_to_steal_index = 0
            + rng.NextDouble() * MathHelper.GetWeight3(stats.rich)
            - rng.NextDouble() * MathHelper.GetWeight3(stats.strength);

            if(want_to_steal_index >= 0) {
                Stealing clone = new Stealing();
                clone.SetUp(sim,[guy,o]);
                return clone;
            }
        }
        return null;
    }

    public void noticed(Simulation sim, int guy) {
        Personality p = sim.people.personalities[guy];
        bool want_to_save = rng.NextDouble() * MathHelper.GetWeight3(p.moral) > 1000;
        bool want_to_act = rng.NextDouble() * MathHelper.GetWeight3(p.confident) > 1000;


        if(want_to_act && want_to_save) {
            discreteness = -10;
            heroes.Add(guy);
            Console.WriteLine($"{guy} hero");
        } else if (want_to_save) {
            discreteness = -10;
            shout.Add(guy);
            Console.WriteLine($"{guy} shout");
        } else {
            ignored.Add(guy);
            Console.WriteLine($"{guy} ignore");
        }
    }

    public void finished() {
        
    }
}