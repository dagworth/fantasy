public class Stealing : IEvent {
    public int discreteness {get; set;}
    public int visibility {get; set;}
    public Person[] participants {get; set;} = [];

    public void SetUp(Simulation sim, Person[] participants) {
        this.participants = participants;
        discreteness = 85;
        visibility = 0;

        Console.WriteLine($"{participants[0].name} trying to steal from {participants[1].name}");
    }

    public IEvent? choose_if_event(Simulation sim, Person guy) {
        Random random = new Random();
        Personality s = guy.personality;
        double stealing_index = 0
            + random.NextDouble() * Math.Pow(s.jealousy,2)
            - random.NextDouble() * Math.Pow(s.moral,3)
            - random.NextDouble() * Math.Pow(s.prideful,2)
            + random.NextDouble() * Math.Pow(s.risky,3)
            + random.NextDouble() * Math.Pow(s.confident,2);
        
        foreach (Person o in guy.location.people) {
            if(o == guy) continue;
            double want_to_steal_index = 0
            + random.NextDouble() * Math.Pow(o.stats.rich,3)
            - random.NextDouble() * Math.Pow(o.stats.strength,3);

            Console.WriteLine($"{guy.name} {stealing_index + want_to_steal_index} {o.name}");
            if(stealing_index + want_to_steal_index > 12000) {
                Stealing clone = new Stealing();
                clone.SetUp(sim,[guy,o]);
                return clone;
            }
        }
        return null;
    }

    public void noticed(Person guy) {
        
    }

    public void finished() {
        
    }
}