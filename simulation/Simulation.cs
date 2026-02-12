using System.Reflection;
using System.Collections.Concurrent;

public class Simulation {
    // private readonly Random random;
    public readonly MemoryManager memoryManager;
    
    public readonly Population people;
    public readonly Memories memories;
    public readonly Modifiers modifiers;
    public readonly Map map;

    private List<IDecisionStep> decision_steps;

    public ConcurrentDictionary<int,Perception> crowd_perceptions;

    public Simulation(int x, int y) {
        map = new Map(x,y);
        people = new Population(this);
        memories = new Memories(this);
        modifiers = new Modifiers();
        // random = new Random();
        crowd_perceptions = [];

        memoryManager = new MemoryManager(this);

        decision_steps = new List<IDecisionStep> {
            new stage1(this),
            new stage2(this),
            new stage3(this),
            new stage4(this)
        };
    }

    public void Simulate() {
        Thread.CurrentThread.Priority = ThreadPriority.Highest;
        foreach (IDecisionStep step in decision_steps) {
            Parallel.For(0, people.people_count, step.execute);
        }
    }

    public void createPerson(Races race, string name) {
        people.createPerson(race, name);
    }
}