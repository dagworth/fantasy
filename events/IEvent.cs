using System.Runtime.CompilerServices;

public interface IEvent : IMemorable {
    int discreteness { get; set; }
    int visibility { get; set; }
    Person[] participants { get; set; }

    void noticed(Person person);
    void finished();
}