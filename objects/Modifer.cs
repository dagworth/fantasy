public class Modifier : IMemorable {
    public int id { get; set; }
    public string name;
    public int condition;

    public Modifier(string name, int condition) {
        id = IdHandler.MakeId(this);
        this.name = name;
        this.condition = condition;
    }
}