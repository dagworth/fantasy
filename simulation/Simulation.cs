using System.Reflection;
using System.Collections.Concurrent;

public class Simulation {
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
        crowd_perceptions = [];

        decision_steps = [
            new stage1(this),
            new stage2(this),
            new stage3(this),
            new stage4(this)
        ];
    }

    public void Simulate() {
        map.ClearEvents();
        foreach (IDecisionStep step in decision_steps) {
            for (int i = 0; i < people.people_count; i++) {
                step.execute(i);
            }
        }
    }

    public int createPerson(Races race, string name) {
        return people.createPerson(race, name);
    }
}