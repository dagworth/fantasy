
var watch = System.Diagnostics.Stopwatch.StartNew();
Console.WriteLine("start");

Simulation simulation = new Simulation(5,5);

for(int i = 0; i < 20; i++){
    simulation.createPerson(Races.Human,"human"+i);
    simulation.createPerson(Races.Elf,"elf"+i);
}

for(int i = 0; i < 2; i++) {
    simulation.createPerson(Races.Demon,"demon"+i);
}

for(int i = 0; i < 2; i++) {
    simulation.Simulate();
}

watch.Stop();
Console.WriteLine($"executed in {watch.ElapsedMilliseconds}ms");