//this stage is when this person gets a perception of everything around him

//this will be what each person thinks about what everyone around them are like, will be helpful in the future

//need to change how theres so many memories of people and mods created here, events and other places need them

using System.Reflection;

public class stage1(Simulation s) : IDecisionStep {
    private Simulation sim = s;
    private Map map = s.map;

    public void execute(int person) {
        sim.crowd_perceptions = [];
        int count = 0;
        ILocation location = sim.people.locations[person];
        Perception crowd_perception = new Perception();

        foreach (int other_guy in location.people) {
            count++;
            if(other_guy == person) continue;

            int memory_of_other_guy = sim.memoryManager.GetMemory(person, other_guy, Memorable.Person);

            //get perception of this guy based on the modifiers that our guy can see
            Perception current_perception = sim.memoryManager.AnalyzeModifiers(person, other_guy, memory_of_other_guy);

            //apply what we know now to change how we think about the other ugy
            sim.memoryManager.ApplyNewPerception(current_perception, memory_of_other_guy);

            //this changes all perception of the modifiers based on how we think about our guy
            sim.memoryManager.UpdateModifierMemory(person, other_guy, current_perception);

            sim.memoryManager.AddPerceptionProperties(crowd_perception, sim.memories.perceptions[memory_of_other_guy]);
            
            sim.crowd_perceptions[person] = crowd_perception;
        }
    }
}