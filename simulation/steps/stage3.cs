//this stage people will decide on how to react on what people arae doing

public class stage3(Simulation s) : IDecisionStep {
    private readonly Simulation sim = s;
    private readonly Random rng = new();

    //this will be changed based on what each person thinks of each situation but we will change thtat in the future
    public void execute(int person) {
        ILocation loc = sim.people.locations[person];
        List<IEvent> events = [];
        foreach(IEvent e in loc.events) {
            double see_index = rng.NextDouble() * sim.people.stats[person].perceptive/e.discreteness;
            if(see_index > 0.7) {
                events.Add(e);
            }
        }

        if(events.Count == 0) return;

        events[rng.Next(events.Count)].noticed(sim, person);
    }
}