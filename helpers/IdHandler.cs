public static class IdHandler {
    private static int id_count = 10000;
    private static readonly Dictionary<int, int> id_to_guy = [];

    public static int GetObjectById(int id) {
        if (id_to_guy.TryGetValue(id, out var guy)) {
            return guy;
        } else {
            Console.WriteLine("This guy did not have an id");
            return 0;
        }
    }

    public static int GetId(Races race) {
        if(race == Races.Human) return 1;
        if(race == Races.Demon) return 2;
        if(race == Races.Elf) return 3;
        return -1;
    }

    public static int GetId(Stat_Mem stat) {
        if(stat == Stat_Mem.smart) return 100;
        if(stat == Stat_Mem.strength) return 101;
        if(stat == Stat_Mem.beauty) return 102;
        return -1;
    }

    public static int GetId(Personality_Mem personality) {
        if(personality == Personality_Mem.confident) return 1001;
        if(personality == Personality_Mem.jealousy) return 1002;
        if(personality == Personality_Mem.prideful) return 1003;
        if(personality == Personality_Mem.spiteful) return 1004;
        if(personality == Personality_Mem.trusting) return 1005;
        if(personality == Personality_Mem.violent) return 1006;
        return -1;
    }
}