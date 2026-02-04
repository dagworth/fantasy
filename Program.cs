using System.Reflection;

var watch = System.Diagnostics.Stopwatch.StartNew();

int width = 5;
int length = 5;
int size = width*length;

int simulation_steps = 100;

Random random = new Random();

ILocation[] map = new ILocation[size];
for(int i = 0; i < size; i++) {
    map[i] = new Lounge();
}

Person createPerson(Races race, string name) {
    RaceData info = DefaultData.get(race);

    Personality personality = new();
    Stats stats = new();

    foreach (PropertyInfo property in typeof(Personality).GetProperties()) {
        double base_val = (double)property.GetValue(info.personality)!;
        double multiplier = .7 + (random.NextDouble() * .6);
        double final_val = (double)Math.Round(base_val * multiplier);
        property.SetValue(personality, final_val);
    }

    foreach (PropertyInfo property in typeof(Stats).GetProperties()) {
        double base_val = (double)property.GetValue(info.stats)!;
        double multiplier = .7 + (random.NextDouble() * .6);
        double final_val = (double)Math.Round(base_val * multiplier);
        property.SetValue(stats, final_val);
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

watch.Stop();
Console.WriteLine($"executed in {watch.ElapsedMilliseconds}ms");