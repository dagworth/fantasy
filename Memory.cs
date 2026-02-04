public interface IMemorable { }

public class Memory(Personality p, Stats s) {
    public int memory_strength;
    public int positive;
    public Dictionary<int,int> associations = [];
    public Personality perception_p = p;
    public Stats perception_s = s;
}