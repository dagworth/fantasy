public class Stealing : IEvent {
    public int discreteness {get; set;}
    public int visibility {get; set;}
    public Person[] participants {get; set;}

    public Stealing(Person[] participants) {
        this.participants = participants;
        discreteness = 85;
        visibility = 0;
    }

    public void noticed(Person guy) {
        Random random = new Random();
        Personality s = guy.personality;
        double stealing_index =
            random.NextDouble() * Math.Pow(s.jealousy,3) + 
            random.NextDouble() * Math.Pow(s.prideful,3);

    }

    public void finished() {
        
    }
}