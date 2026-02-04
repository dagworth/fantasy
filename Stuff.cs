public class Stuff : IMemorable {
    public int id;
    public Stuff() {
        id = IdHandler.MakeId(this);
    }
}