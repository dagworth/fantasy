public interface IMemorable {
    int id { get; set; }
}

public class Memory {
    public int memory_strength;
    public int positive;
    public Dictionary<int,int> associations = [];
    public Perception perception = new Perception();
}