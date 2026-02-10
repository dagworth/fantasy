using System.Reflection;
using System.Globalization;

public static class EventDecisionLoader {
    private static readonly Dictionary<Func<Simulation,int,IEvent?>,Type> events = [];

    static EventDecisionLoader() {
        var assembly = Assembly.GetExecutingAssembly();

        var classes = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && typeof(IEvent).IsAssignableFrom(t));
        
        foreach (var cool in classes) {
            IEvent instance = (IEvent)Activator.CreateInstance(cool)!;
            events[instance.choose_if_event] = cool;
        }
    }

    public static Dictionary<Func<Simulation,int,IEvent?>,Type> GetEventDecisions() {
        return events;
    }
}