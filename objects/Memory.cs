public class Memory {
    public List<int> memory_strengths = [];
    public List<int> positivity = [];
    public List<Dictionary<int,int>> associations = [];
    public List<Perception> perceptions = [];

    public void CreateMemory(int id, Perception p) {
        memory_strengths[id] = 0;
        positivity[id] = 0;
        associations[id] = new();
        perceptions[id] = p;
    }
}