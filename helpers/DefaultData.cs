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
                },
                new Stats() {
                    smart = 20,
                    strength = 10,
                    beauty = 20
                }
            )
        },
        { Races.Elf, new(
                new Personality() {
                    violent = -50,
                    jealousy = 0,
                    spiteful = 0,
                    prideful = 80,
                    trusting = 0,
                    confident = 90,
                },
                new Stats() {
                    smart = 70,
                    strength = 0,
                    beauty = 50,
                }
            )
        },
        { Races.Demon, new(
                new Personality() {
                    violent = 100,
                    jealousy = 100,
                    spiteful = 100,
                    prideful = 100,
                    trusting = -100,
                    confident = 100,
                },
                new Stats() {
                    smart = 100,
                    strength = 100,
                    beauty = -100
                }
            )
        },
    };
    
    public static RaceData get(Races race) {
        return info[race];
    }
}