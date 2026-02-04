//this stage is when this person gets a perception of everything around him

//this will be what each person thinks about what everyone around them are like, will be helpful in the future

using System.Net;
using System.Reflection;
using System.Security.Cryptography;

public static class stage1 {
    private static Dictionary<Person, Perception> crowd_perceptions = [];
    private static double dampen(double value, double magnitude) {
        if (value < 0) {
            return -Math.Pow(-value, magnitude);
        } else {
            return Math.Pow(value, magnitude);
        }
    }

    public static void execute(Person person) {
        int count = 0;
        Dictionary<int,Memory> memories = person.memories;
        ILocation location = person.location;
        Perception crowd_perception = new Perception();

        foreach (Person other_guy in location.people) {
            count++;
            if(other_guy == person) continue;

            Perception current_perception = new Perception();

            foreach (Modifier mod in other_guy.modifiers) {
                if (memories.TryGetValue(other_guy.id,out var memory)) {
                    foreach (FieldInfo field in typeof(Perception).GetFields()) {
                        int val = (int)field.GetValue(memory.perception)!;

                        //this equation probably should be changed later
                        field.SetValue(current_perception, val);
                    }
                } else {
                    //make new memory for that modifier with no perception
                    Memory new_memory = new Memory(current_perception);
                    memories[mod.id] = new_memory;
                }
            }

            Memory memory_of_other_guy;

            if(memories.TryGetValue(other_guy.id,out var a)) {
                Console.WriteLine($"{person.id} already has memory for {other_guy.id}");
                memory_of_other_guy = a;
                foreach (FieldInfo field in typeof(Perception).GetFields()) {
                    int current = (int)field.GetValue(current_perception)!;
                    int other = (int)field.GetValue(memory_of_other_guy.perception)!;
                    //we dont want our perception of the guy be changing too much every time we see them
                    double difference = dampen(current - other,1/3);

                    field.SetValue(memory_of_other_guy.perception, current + difference);
                }
            } else {
                Console.WriteLine($"{person.id} cretes new memory for {other_guy.id}");
                Memory new_memory = new Memory(current_perception);
                memories[other_guy.id] = new_memory;
                memory_of_other_guy = new_memory;
            }

            //this second loop changes all perception of the modifiers based on how we think about our guy
            foreach (Modifier mod in other_guy.modifiers) {
                foreach (FieldInfo field in typeof(Perception).GetFields()) {
                    int current = (int)field.GetValue(memory_of_other_guy)!;
                    int other = (int)field.GetValue(memories[mod.id].perception)!;
                    double difference = dampen(current - other,1/4);

                    //change for future
                    field.SetValue(memory_of_other_guy.perception, current + difference);
                }
            }

            foreach (FieldInfo field in typeof(Perception).GetFields()) {
                int current = (int)field.GetValue(crowd_perception)!;
                int other_guy_current = (int)field.GetValue(memory_of_other_guy.perception)!;
                field.SetValue(crowd_perception,current + other_guy_current);
            }

            crowd_perceptions[person] = crowd_perception;
        }
    }
}