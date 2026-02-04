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
                    foreach (PropertyInfo property in typeof(Perception).GetProperties()) {
                        double val = (double)property.GetValue(memory.perception)!;

                        //this equation probably should be changed later
                        property.SetValue(current_perception, val);
                    }
                } else {
                    //make new memory for that modifier with no perception
                    Memory new_memory = new Memory(current_perception);
                    memories[mod.id] = new_memory;
                }
            }

            Memory memory_of_other_guy;

            if(memories.TryGetValue(other_guy.id,out var a)) {
                //Console.WriteLine($"{person.id} already has memory for {other_guy.id}");
                memory_of_other_guy = a;
                foreach (PropertyInfo property in typeof(Perception).GetProperties()) {
                    double current = (double)property.GetValue(current_perception)!;
                    double other = (double)property.GetValue(memory_of_other_guy.perception)!;
                    //we dont want our perception of the guy be changing too much every time we see them
                    double difference = dampen(current - other,1.0/3);

                    property.SetValue(memory_of_other_guy.perception, current + difference);
                }
            } else {
                // Console.WriteLine($"{person.id} cretes new memory for {other_guy.id}");
                Memory new_memory = new Memory(current_perception);
                memories[other_guy.id] = new_memory;
                memory_of_other_guy = new_memory;
            }

            //this second loop changes all perception of the modifiers based on how we think about our guy
            foreach (Modifier mod in other_guy.modifiers) {
                foreach (PropertyInfo property in typeof(Perception).GetProperties()) {
                    double current = (double)property.GetValue(memory_of_other_guy)!;
                    double other = (double)property.GetValue(memories[mod.id].perception)!;
                    double difference = dampen(current - other,1/4);

                    //change for future
                    property.SetValue(memory_of_other_guy.perception, current + difference);
                }
            }

            foreach (PropertyInfo property in typeof(Perception).GetProperties()) {
                double current = (double)property.GetValue(crowd_perception)!;
                double other_guy_current = (double)property.GetValue(memory_of_other_guy.perception)!;
                property.SetValue(crowd_perception,current + other_guy_current);
            }

            crowd_perceptions[person] = crowd_perception;
        }
    }
}