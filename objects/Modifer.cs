using System.Collections.Concurrent;

public class Modifiers {
    private int count = 0;
    public ConcurrentDictionary<int,string> names = [];
    public ConcurrentDictionary<int,int> conditions = [];

    public int CreateModifier() {
        int id = count++;
        names[id] = "u";
        conditions[id] = 0;

        return id;
    }
}