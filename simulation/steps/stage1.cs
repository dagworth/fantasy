//this stage is when this person gets a perception of everything around him

//this will be what each person thinks about what everyone around them are like, will be helpful in the future

//need to change how theres so many memories of people and mods created here, events and other places need them

using System.Reflection;

public class stage1(Simulation s) : IDecisionStep {
    private Simulation sim = s;
    private Population population = s.people;
    private Map map = s.map;

    public void execute(int person) {
        sim.crowd_perceptions = [];
        int count = 0;
        ILocation location = map.GetLocation(population.location_index[person]);
        Perception crowd_perception = new Perception();

        foreach (int other_guy in location.people) {
            count++;
            if(other_guy == person) continue;

            Memory memory_of_other_guy = MemoryManager.GetMemory(person, other_guy);

            //get perception of this guy based on the modifiers that our guy can see
            Perception current_perception = MemoryManager.AnalyzeModifiers(person, other_guy, memory_of_other_guy);

            //apply what we know now to change how we think about the other ugy
            MemoryManager.ApplyNewPerception(current_perception, memory_of_other_guy);

            //this changes all perception of the modifiers based on how we think about our guy
            MemoryManager.UpdateModifierMemory(person, other_guy, current_perception);

            MemoryManager.AddPerceptionProperties(crowd_perception, memory_of_other_guy.perception);
            sim.crowd_perceptions[person] = crowd_perception;
        }
    }
}