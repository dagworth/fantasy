using System.Reflection;

public static class MemoryManager {
    private static PropertyInfo[] perception_properties;

    static MemoryManager() {
        perception_properties = typeof(Perception).GetProperties();
    }

    private static double dampen(double value, double magnitude) {
        if (value < 0) {
            return -Math.Pow(-value, magnitude);
        } else {
            return Math.Pow(value, magnitude);
        }
    }

    public static Memory GetMemory(Person p, IMemorable o) {
        if(p.memories.TryGetValue(o.id,out var a)) {
            return a;
        } else {
            Memory new_memory = new();
            p.memories[o.id] = new_memory;
            return new_memory;
        }
    }

    public static Perception AnalyzeModifiers(Person p, Person other_guy, Memory mem) {
        //p will be used later based on what he feels about these items i guess
        Perception current_perception = new();
        foreach (Modifier mod in other_guy.modifiers) {
            foreach (PropertyInfo property in perception_properties) {
                //double current = (double)property.GetValue(current_perception)!;
                //double memory_perception = (double)property.GetValue(mem.perception)!;

                //this equation probably should be changed later,
                //outlier can bring everything up
                //property.SetValue(current_perception, current + memory_perception);
            }
        }
        return current_perception;
    }

    public static void ApplyNewPerception(Perception current_perception, Memory mem) {
        foreach (PropertyInfo property in perception_properties) {
            //double current = (double)property.GetValue(current_perception)!;
            //double other = (double)property.GetValue(mem.perception)!;
            //double difference = dampen(current - other,.3);

            //tjhere needs to be another dampen here or something, same problem as modifiers
            //property.SetValue(mem.perception, current + difference);
        }
    }

    public static void UpdateModifierMemory(Person p, Person other_guy, Perception current_perception) {
        Memory memory_of_other_guy = GetMemory(p,other_guy);
        foreach (Modifier mod in other_guy.modifiers) {
            foreach (PropertyInfo property in perception_properties) {
                //double current = (double)property.GetValue(p.memories[mod.id].perception)!;
                //double other = (double)property.GetValue(current_perception)!;
                //double difference = dampen(current - other,1/4);

                //change for future
                //property.SetValue(memory_of_other_guy.perception, current + difference);
            }
        }
    }

    public static void AddPerceptionProperties(Perception a, Perception b) {
        foreach (PropertyInfo property in typeof(Perception).GetProperties()) {
            //double c = (double)property.GetValue(a)!;
            //double d = (double)property.GetValue(b)!;

            //there needs to be an average out here or smth
            //property.SetValue(a, c + d);
        }
    }
}