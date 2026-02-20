
var watch = System.Diagnostics.Stopwatch.StartNew();
Console.WriteLine("start");

Simulation simulation = new Simulation(30,30);

void print(int input) {
    Console.WriteLine(simulation.people.names[input]);
    for(int i = 0; i < Accessors.personality_accessors.Count; i++) {
        var (get, set) = Accessors.personality_accessors[i];
        Console.Write($"({Accessors.personality_names[i]}, {get(simulation.people.personalities[input])}) ");
    }
    Console.WriteLine();
    for(int i = 0; i < Accessors.stats_accessors.Count; i++) {
        var (get, set) = Accessors.stats_accessors[i];
        Console.Write($"({Accessors.stats_names[i]}, {get(simulation.people.stats[input])}) ");
    }
    Console.WriteLine();
}

for(int i = 0; i < 6000; i++){
    simulation.createPerson(Races.Human,"human"+i);
    simulation.createPerson(Races.Elf,"elf"+i);

}

for(int i = 0; i < 20; i++) {
    simulation.createPerson(Races.Demon,"demon"+i);
}

for(int i = 0; i < 50; i++) {
    // Console.WriteLine($"step {i} start");
    simulation.Simulate();
}

watch.Stop();
Console.WriteLine($"executed in {watch.ElapsedMilliseconds}ms");

while (true) {
    int input = int.Parse(Console.ReadLine()!);
    print(input);
}