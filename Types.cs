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
    int violent { get; set; }
    int jealousy { get; set; }
    int spiteful { get; set; }
    int prideful { get; set; }
    int trusting { get; set; }
    int confident { get; set; }
}

public interface IStats {
    int smart { get; set; }
    int strength { get; set; }
    int beauty { get; set; }
}

public class Personality : IPersonality {
    public int violent { get; set; }
    public int jealousy { get; set; }
    public int spiteful { get; set; }
    public int prideful { get; set; }
    public int trusting { get; set; }
    public int confident { get; set; }
}

public class Stats : IStats {
    public int smart { get; set; }
    public int strength { get; set; }
    public int beauty { get; set; }
}

public class Perception : IPersonality, IStats {
    public int smart { get; set; }
    public int strength { get; set; }
    public int beauty { get; set; }
    
    public int violent { get; set; }
    public int jealousy { get; set; }
    public int spiteful { get; set; }
    public int prideful { get; set; }
    public int trusting { get; set; }
    public int confident { get; set; }
}