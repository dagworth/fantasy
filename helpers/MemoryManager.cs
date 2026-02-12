using System.Reflection;
using System.Linq.Expressions;

public class MemoryManager {
    private static List<(Func<Perception, double> Get, Action<Perception, double> Set)> perception_accessors = [];
    private Simulation sim;

    static MemoryManager() {
        var perception_properties = typeof(Perception).GetProperties();
        foreach (var prop in perception_properties) {
            var instance = Expression.Parameter(typeof(Perception), "p");
            var getExpr = Expression.Property(instance, prop);
            var value = Expression.Parameter(typeof(double), "v");
            var setExpr = Expression.Assign(getExpr, value);

            perception_accessors.Add(
                (
                    Expression.Lambda<Func<Perception, double>>(getExpr, instance).Compile(),
                    Expression.Lambda<Action<Perception, double>>(setExpr, instance, value).Compile()
                )
            );
        }
    }

    public MemoryManager(Simulation sim) {
        this.sim = sim;
    }

    public int GetMemory(int p, int o, Memorable type) {
        if(sim.people.memories[p].TryGetValue(o,out var a)) {
            return a;
        } else {
            return sim.memories.CreateMemory(p,o,type,new Perception());
        }
    }

    public Perception AnalyzeModifiers(int p, int other_guy, int mem_id) {
        //p will be used later based on what he feels about these items i guess
        Perception current_perception = new();
        foreach (int mod_id in sim.people.modifiers[other_guy]) {
            foreach (var (get, set) in perception_accessors) {
                double current = get(current_perception);
                double memory_perception = get(sim.memories.perceptions[mem_id]);

                set(current_perception, current + memory_perception);
            }
        }
        return current_perception;
    }

    public void ApplyNewPerception(Perception current_perception, int mem_id) {
        foreach (var (get, set) in perception_accessors) {
            double current = get(current_perception);
            //Console.WriteLine($"{mem_id} try in {sim.memories.perceptions.Count}");
            double other = get(sim.memories.perceptions[mem_id]);
            double diff = MathHelper.dampen(current-other,.3);

            set(sim.memories.perceptions[mem_id], current + diff);
        }
    }

    public void UpdateModifierMemory(int p, int other_guy_id, Perception current_perception) {
        foreach (int mod_id in sim.people.modifiers[other_guy_id]) {
            foreach (var (get, set) in perception_accessors) {
                double current = get(sim.memories.perceptions[mod_id]);
                double other = get(current_perception);
                double diff = MathHelper.dampen(current-other,.25);

                set(sim.memories.perceptions[other_guy_id], current + diff);
            }
        }
    }

    public void AddPerceptionProperties(Perception a, Perception b) {
        foreach (var (get, set) in perception_accessors) {
            double c = get(a);
            double d = get(b);

            set(a, c+d);
        }
    }
}