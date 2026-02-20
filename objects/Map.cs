public class Map {
    private List<ILocation> locations;
    public int x;
    public int y;
    public int size;

    public Map(int x, int y) {
        this.x = x;
        this.y = y;
        size = x*y;
        locations = [];
        for(int i = 0; i < size; i++) {
            locations.Add(new Lounge(i));
        }
    }

    public ILocation GetLocation(int index) {
        return locations[index];
    }

    public List<ILocation> GetLocations() {
        return locations;
    }

    public void ClearEvents() {
        foreach (var item in locations)
        {
            item.events = [];
        }
    }
}