public class RaceData(Personality p, Stats s) {
    public Personality personality = p;
    public Stats stats = s;
}

public static class DefaultData {
    public static readonly Dictionary<Races,RaceData> info = new Dictionary<Races,RaceData>{
        { Races.Human, new(
                new Personality() {
                    violent = 10,
                    jealousy = 50,
                    spiteful = 20,
                    prideful = 50,
                    trusting = 30,
                    confident = 20,
                    moral = 50,
                    risky = 25,
                },
                new Stats() {
                    smart = 20,
                    strength = 10,
                    beauty = 20,
                    rich = 20,
                    perceptive = 40,
                }
            )
        },
        { Races.Elf, new(
                new Personality() {
                    violent = -50,
                    jealousy = 30,
                    spiteful = 30,
                    prideful = 80,
                    trusting = 30,
                    confident = 90,
                    moral = 50,
                    risky = -50,
                },
                new Stats() {
                    smart = 70,
                    strength = 30,
                    beauty = 50,
                    rich = 40,
                    perceptive = 70,
                }
            )
        },
        { Races.Demon, new(
                new Personality() {
                    violent = 100,
                    jealousy = 40,
                    spiteful = 80,
                    prideful = 90,
                    trusting = -40,
                    confident = 90,
                    moral = -50,
                    risky = 40,
                },
                new Stats() {
                    smart = 100,
                    strength = 100,
                    beauty = -10,
                    rich = -30,
                    perceptive = 100,
                }
            )
        },
    };
    
    public static RaceData get(Races race) {
        return info[race];
    }
}