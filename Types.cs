public enum Memorable {
    Races,
    Stat,
    Personality,
    Location,
    Event,
    Person,
    Stuff
}

public enum Races {
    Human,
    Elf,
    Demon
}

public enum Stat_Mem {
    smart,
    strength,
    beauty
}

public enum Personality_Mem {
    violent,
    jealousy,
    spiteful,
    prideful,
    trusting,
    confident
}

public interface IPersonality {
    double violent { get; set; }
    double jealousy { get; set; }
    double spiteful { get; set; }
    double prideful { get; set; }
    double trusting { get; set; }
    double confident { get; set; }
    double moral { get; set; }
    double risky { get; set; }
}

public interface IStats {
    double smart { get; set; }
    double strength { get; set; }
    double beauty { get; set; }
    double rich { get; set; }
    double perceptive { get; set; }
}

public class Personality : IPersonality {
    public double violent { get; set; }
    public double jealousy { get; set; }
    public double spiteful { get; set; }
    public double prideful { get; set; }
    public double trusting { get; set; }
    public double confident { get; set; }
    public double moral { get; set; }
    public double risky { get; set; }
}

public class Stats : IStats {
    public double smart { get; set; }
    public double strength { get; set; }
    public double beauty { get; set; }
    public double rich { get; set; }
    public double perceptive { get; set; }
}

public struct Perception : IPersonality, IStats {
    public double smart { get; set; }
    public double strength { get; set; }
    public double beauty { get; set; }
    public double rich { get; set; }
    public double perceptive { get; set; }

    public double violent { get; set; }
    public double jealousy { get; set; }
    public double spiteful { get; set; }
    public double prideful { get; set; }
    public double trusting { get; set; }
    public double confident { get; set; }
    public double moral { get; set; }
    public double risky { get; set; }
}