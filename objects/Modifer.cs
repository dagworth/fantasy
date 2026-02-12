public class Modifiers {
    private int count = 0;
    public List<string> names = [];
    public List<int> conditions = [];

    public int CreateModifier() {
        names.Add("u");
        conditions.Add(0);

        return count++;
    }
}