public class Stuff : IMemorable {
    public int id { get; set; }
    public Stuff() {
        id = IdHandler.MakeId(this);
    }
}