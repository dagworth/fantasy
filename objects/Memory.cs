using System.Collections.Concurrent;

public class Memories(Simulation s) {
    private Simulation sim = s;
    private int count = 0;
    public ConcurrentDictionary<int,int> memory_strengths = [];
    public ConcurrentDictionary<int,Memorable> memory_types = [];
    public ConcurrentDictionary<int,int> positivity = [];
    public ConcurrentDictionary<int,ConcurrentDictionary<int,int>> associations = [];
    public ConcurrentDictionary<int,Perception> perceptions = [];

    public int CreateMemory(int owner, int subject, Memorable mem_type, Perception p) {
        int id = count++;
        sim.people.memories[owner][subject] = id;
        memory_types[id] = mem_type;
        positivity[id] = 0;
        associations[id] = [];
        perceptions[id] = p;
        return id;
    }
}