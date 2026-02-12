public class Map {
    private ILocation[] locations;
    public int x;
    public int y;
    public int size;

    public Map(int x, int y) {
        this.x = x;
        this.y = y;
        size = x*y;
        locations = new ILocation[size];
        for(int i = 0; i < size; i++) {
            locations[i] = new Lounge(i);
        }
    }

    public ILocation GetLocation(int index) {
        return locations[index];
    }
}