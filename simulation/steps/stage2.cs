//this stage is when everyone will decide what to do based on the crowd_perception

//lots of stuff can be cached here somewhere

public class stage2(Simulation s) : IDecisionStep {
    private readonly Simulation sim = s;
    public void execute(int person) {
        List<Func<Simulation,int,IEvent?>> dict = EventDecisionLoader.GetEventStartConditions();
        //check is the function each location should have
        foreach (Func<Simulation,int,IEvent?> check in dict) {
            IEvent? result = check.Invoke(sim,person);
            if (result != null) {
                sim.people.locations[person].events.Add(result);
                return;
            }
        }
    }
}