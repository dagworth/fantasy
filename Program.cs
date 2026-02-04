using System.Reflection;

int width = 5;
int length = 5;
int size = width*length;

int simulation_steps = 2;

Random random = new Random();

ILocation[] map = new ILocation[size];
for(int i = 0; i < size; i++) {
    map[i] = new Lounge();
}

Person createPerson(Races race, string name) {
    RaceData info = DefaultData.get(race);

    Personality personality = new();
    Stats stats = new();

    FieldInfo[] personality_fields = typeof(Personality).GetFields();
    foreach (FieldInfo field in personality_fields) {
        int base_val = (int)field.GetValue(info.personality)!;
        double multiplier = .7 + (random.NextDouble() * .6);
        int final_val = (int)Math.Round(base_val * multiplier);
        field.SetValue(personality, final_val);
    }

    FieldInfo[] stats_fields = typeof(Stats).GetFields();
    foreach (FieldInfo field in stats_fields) {
        int base_val = (int)field.GetValue(info.stats)!;
        double multiplier = .7 + (random.NextDouble() * .6);
        int final_val = (int)Math.Round(base_val * multiplier);
        field.SetValue(stats, final_val);
    }

    ILocation loc = map[random.Next(0,size)];
    Person clone = new(name, race, loc, personality, stats, random.Next(0,70) + 30);
    loc.people.Add(clone);

    return clone;
}

List<Person> people = [];

for(int i = 0; i < 40; i++){
   people.Add(createPerson(Races.Human,"human"+i));
   people.Add(createPerson(Races.Elf,"elf"+i));
   people.Add(createPerson(Races.Demon,"demon"+i));
}

for(int i = 0; i < simulation_steps; i++) {
    foreach (Person a in people) {
        stage1.execute(a);
    }
}

