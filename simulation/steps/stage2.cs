//this stage is when everyone will decide what to do based on the crowd_perception

using System.Threading.Tasks.Dataflow;

public class stage2(Simulation s) : IDecisionStep {
    private Simulation sim = s;
    public void execute(int person) {
        var dict = EventDecisionLoader.GetEventDecisions();
        foreach (KeyValuePair<Func<Simulation,int,IEvent?>,Type> info in dict) {
            IEvent? result = info.Key.Invoke(sim,person);
            if (result != null) {
                sim.people.locations[person].events.Add(result);
                return;
            }
        }
    }
}