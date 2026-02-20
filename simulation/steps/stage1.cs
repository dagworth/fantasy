//this stage is when this person gets a perception of everything around him

public class stage1(Simulation s) : IDecisionStep {
    private readonly Simulation sim = s;
    public void execute(int p) {
        sim.crowd_perceptions = [];
        ILocation location = sim.people.locations[p];

        //this will be what each person thinks about what everyone around them, will be helpful in the future
        Perception crowd_perception = new Perception();

        foreach (int other in location.people) {
            if(p == other) continue;

            //create or get memory of other_guy
            int memory_of_o = sim.memories.GetMemory(p, other, Memorable.Person);

            //create current perception of the guy based on what person sees
            Perception current_perception = GetCurrentPerception(memory_of_o, other);

            //apply what we know now to change how we think about the other ugy
            ApplyNewPerceptionToOther(memory_of_o, current_perception);

            // //this changes all perception of the modifiers based on how we think about our guy
            UpdateModifiersPerception(other, current_perception);

            // //adds current perception to crowd_perception
            UpdateCrowdPerception(crowd_perception, sim.memories.perceptions[memory_of_o]);
        }

        sim.crowd_perceptions[p] = crowd_perception;
    }

    public Perception GetCurrentPerception(int mem_id, int other_guy) {
        Perception current_perception = new();
        foreach (int mod_id in sim.people.modifiers[other_guy]) {
            foreach (var (get, set) in Accessors.perception_accessors) {
                double current = get(current_perception);
                double memory_perception = get(sim.memories.perceptions[mem_id]);

                set(current_perception, current + memory_perception);
            }
        }
        return current_perception;
    }

    public void ApplyNewPerceptionToOther(int mem_id, Perception current_perception) {
        foreach (var (get, set) in Accessors.perception_accessors) {
            double current = get(current_perception);
            double other = get(sim.memories.perceptions[mem_id]);
            double diff = MathHelper.dampen(current-other,.3);

            set(sim.memories.perceptions[mem_id], current + diff);
        }
    }

    public void UpdateModifiersPerception(int other_guy_id, Perception current_perception) {
        foreach (int mod_id in sim.people.modifiers[other_guy_id]) {
            foreach (var (get, set) in Accessors.perception_accessors) {
                double current = get(sim.memories.perceptions[mod_id]);
                double other = get(current_perception);
                double diff = MathHelper.dampen(current-other,.25);

                set(sim.memories.perceptions[other_guy_id], current + diff);
            }
        }
    }

    public void UpdateCrowdPerception(Perception a, Perception b) {
        foreach (var (get, set) in Accessors.perception_accessors) {
            double c = get(a);
            double d = get(b);

            set(a, c+d);
        }
    }
}