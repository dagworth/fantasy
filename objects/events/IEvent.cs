using System.Runtime.CompilerServices;

public interface IEvent {
    int discreteness { get; set; }
    int visibility { get; set; }
    int[] participants { get; set; }

    void noticed(int person);
    void SetUp(Simulation sim, int[] participants);
    IEvent? choose_if_event(Simulation sim, int guy);
    void finished();
}