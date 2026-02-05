public interface IMemorable { }

public class Memory(Perception p) {
    public int memory_strength;
    public int positive;
    public Dictionary<int,int> associations = [];
    public Perception perception = p;
}