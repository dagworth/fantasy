using System.Reflection;
using System.Runtime.Intrinsics.X86;

public class MemoryManager {
    private static PropertyInfo[] perception_properties;

    private Simulation sim;

    static MemoryManager() {
        perception_properties = typeof(Perception).GetProperties();
        FastCompile.init(perception_properties);
    }

    public MemoryManager(Simulation sim) {
        this.sim = sim;
    }

    public Memory GetMemory(int p, int o) {
        if(sim.people.memories[p].TryGetValue(o,out var a)) {
            return a;
        } else {
            Memory new_memory = new();
            sim.people.memories[p][o] = new_memory;
            return new_memory;
        }
    }

    public Perception AnalyzeModifiers(int p, int other_guy, Memory mem) {
        //p will be used later based on what he feels about these items i guess
        Perception current_perception = new();
        foreach (Modifier mod in sim.people.modifiers[other_guy]) {
            foreach (var (get, set) in FastCompile.perception_accessors) {
                double current = get(current_perception);
                double memory_perception = get(mem.perception);

                set(current_perception, current + memory_perception);
            }
        }
        return current_perception;
    }

    public void ApplyNewPerception(Perception current_perception, Memory mem) {
        foreach (var (get, set) in FastCompile.perception_accessors) {
            double current = get(current_perception);
            double other = get(mem.perception);
            double diff = MathHelper.dampen(current-other,.3);

            set(mem.perception, current + diff);
        }
    }

    public void UpdateModifierMemory(int p, int other_guy, Perception current_perception) {
        Memory memory_of_other_guy = GetMemory(p,other_guy);
        foreach (Modifier mod in other_guy.modifiers) {
            foreach (var (get, set) in FastCompile.perception_accessors) {
                double current = get(p.memories[mod.id].perception);
                double other = get(current_perception);
                double diff = dampen(current-other,.25);

                set(memory_of_other_guy.perception, current + diff);
            }
        }
    }

    public void AddPerceptionProperties(Perception a, Perception b) {
        foreach (var (get, set) in FastCompile.perception_accessors) {
            double c = get(a);
            double d = get(b);

            set(a, c+d);
        }
    }
}