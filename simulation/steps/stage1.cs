//this stage is when this person gets a perception of everything around him

//this will be what each person thinks about what everyone around them are like, will be helpful in the future

using System.Net;
using System.Reflection;
using System.Security.Cryptography;

//need to change how theres so many memories of people and mods created here, events and other places need them

public class stage1(Simulation s) : IDecisionStep {
    private Simulation sim = s;
    private double dampen(double value, double magnitude) {
        if (value < 0) {
            return -Math.Pow(-value, magnitude);
        } else {
            return Math.Pow(value, magnitude);
        }
    }

    public void execute(Person person) {
        sim.crowd_perceptions = [];
        int count = 0;
        Dictionary<int,Memory> memories = person.memories;
        ILocation location = person.location;
        Perception crowd_perception = new Perception();

        foreach (Person other_guy in location.people) {
            count++;
            if(other_guy == person) continue;

            Perception current_perception = new Perception();

            //look at the guys modifiers
            foreach (Modifier mod in other_guy.modifiers) {
                if (memories.TryGetValue(other_guy.id,out var memory)) {
                    foreach (PropertyInfo property in typeof(Perception).GetProperties()) {
                        double current = (double)property.GetValue(current_perception)!;
                        double memory_perception = (double)property.GetValue(memory.perception)!;

                        //this equation probably should be changed later,
                        //outlier can bring everything up
                        property.SetValue(current_perception, current + memory_perception);
                    }
                } else {
                    Memory new_memory = new Memory(current_perception);
                    memories[mod.id] = new_memory;
                }
            }

            //look at what we know about this guy
            Memory memory_of_other_guy;
            if(memories.TryGetValue(other_guy.id,out var a)) {
                memory_of_other_guy = a;
                // foreach (PropertyInfo property in typeof(Perception).GetProperties()) {
                //     double current = (double)property.GetValue(current_perception)!;
                //     double other = (double)property.GetValue(memory_of_other_guy.perception)!;
                //     double difference = dampen(current - other,1.0/3);

                //     //tjhere needs to be another dampen here or something, same problem as modifiers
                //     property.SetValue(memory_of_other_guy.perception, current + difference);
                // }
            } else {
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
                double current_crowd = (double)property.GetValue(crowd_perception)!;
                double other_guy_current = (double)property.GetValue(memory_of_other_guy.perception)!;

                //there needs to be an average out here or smth
                property.SetValue(crowd_perception,current_crowd + other_guy_current);
            }

            sim.crowd_perceptions[person] = crowd_perception;
        }
    }
}