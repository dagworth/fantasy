using System.Reflection;
using System.Globalization;

public static class EventDecisionLoader {
    private static readonly List<Func<Simulation,int,IEvent?>> event_starts = [];

    static EventDecisionLoader() {
        var assembly = Assembly.GetExecutingAssembly();

        var classes = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && typeof(IEvent).IsAssignableFrom(t));
        
        foreach (var cool in classes) {
            IEvent instance = (IEvent)Activator.CreateInstance(cool)!;
            event_starts.Add(instance.choose_if_event);
        }
    }

    public static List<Func<Simulation,int,IEvent?>> GetEventStartConditions() {
        return event_starts;
    }
}