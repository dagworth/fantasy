public class Stealing : IEvent {
    public int id { get; set; }

    public int discreteness {get; set;}
    public int visibility {get; set;}
    public int[] participants {get; set;} = [];

    public void SetUp(Simulation sim, int[] participants) {
        this.participants = participants;
        discreteness = 85;
        visibility = 0;

        Console.WriteLine($"{participants[0]} trying to steal from {participants[1]}");
    }

    public IEvent? choose_if_event(Simulation sim, int guy) {
        Random random = new Random();
        Personality s = sim.people.personalities[guy];
        double stealing_index = 0
            + random.NextDouble() * Math.Pow(s.jealousy,2)
            - random.NextDouble() * Math.Pow(s.moral,3)
            - random.NextDouble() * Math.Pow(s.prideful,2)
            + random.NextDouble() * Math.Pow(s.risky,3)
            + random.NextDouble() * Math.Pow(s.confident,2);
        

        
        foreach (int o in sim.people.locations[guy].people) {
            if(o == guy) continue;
            double want_to_steal_index = 0
            + random.NextDouble() * Math.Pow(sim.people.stats[o].rich,3)
            - random.NextDouble() * Math.Pow(sim.people.stats[o].strength,3);

            //Console.WriteLine($"{guy.name} {stealing_index + want_to_steal_index} {o.name}");
            if(stealing_index + want_to_steal_index > 12000) {
                Stealing clone = new Stealing();
                clone.SetUp(sim,[guy,o]);
                return clone;
            }
        }
        return null;
    }

    public void noticed(int guy) {
        
    }

    public void finished() {
        
    }
}