using System.Collections.Concurrent;

public class Memories(Simulation s) {
    private Simulation sim = s;
    public static readonly Perception default_perception = new Perception();

    private int count = 0;
    public ConcurrentDictionary<int,int> memory_strengths = new(Environment.ProcessorCount, 100000);
    public ConcurrentDictionary<int,Memorable> memory_types = new(Environment.ProcessorCount, 100000);
    public ConcurrentDictionary<int,int> positivity = new(Environment.ProcessorCount, 100000);
    public ConcurrentDictionary<int,Dictionary<int,int>> associations = new(Environment.ProcessorCount, 100000);
    public ConcurrentDictionary<int,Perception> perceptions = new(Environment.ProcessorCount, 100000);

    public int CreateMemory(int owner, int subject, Memorable mem_type) {
        int id = Interlocked.Increment(ref count);
        sim.people.memories[owner][subject] = id;
        memory_types[id] = mem_type;
        positivity[id] = 0;
        associations[id] = [];
        perceptions[id] = default_perception;
        return id;
    }

    public int GetMemory(int p, int o, Memorable type) {
        if(sim.people.memories[p].TryGetValue(o,out var a)) {
            return a;
        } else {
            return CreateMemory(p,o,type);
        }
    }
}