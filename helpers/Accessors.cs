using System.Reflection;
using System.Linq.Expressions;
using System.Security.Cryptography;

public static class Accessors {
    public static readonly List<(Func<Perception, double> Get, Action<Perception, double> Set)> perception_accessors = [];
    public static readonly List<(Func<Personality, double> Get, Action<Personality, double> Set)> personality_accessors = [];
    public static readonly List<(Func<Stats, double> Get, Action<Stats, double> Set)> stats_accessors = [];

    public static readonly List<string> perception_names = [];
    public static readonly List<string> personality_names = [];
    public static readonly List<string> stats_names = [];

    static Accessors() {
        var perception_properties = typeof(Perception).GetProperties();
        foreach (var prop in perception_properties) {
            var instance = Expression.Parameter(typeof(Perception), "p");
            var getExpr = Expression.Property(instance, prop);
            var value = Expression.Parameter(typeof(double), "v");
            var setExpr = Expression.Assign(getExpr, value);

            perception_accessors.Add(
                (
                    Expression.Lambda<Func<Perception, double>>(getExpr, instance).Compile(),
                    Expression.Lambda<Action<Perception, double>>(setExpr, instance, value).Compile()
                )
            );
            perception_names.Add(prop.Name!);
        }

        var personality_properties = typeof(Personality).GetProperties();
        foreach (var prop in personality_properties) {
            var instance = Expression.Parameter(typeof(Personality), "p");
            var getExpr = Expression.Property(instance, prop);
            var value = Expression.Parameter(typeof(double), "v");
            var setExpr = Expression.Assign(getExpr, value);

            personality_accessors.Add(
                (
                    Expression.Lambda<Func<Personality, double>>(getExpr, instance).Compile(),
                    Expression.Lambda<Action<Personality, double>>(setExpr, instance, value).Compile()
                )
            );
            personality_names.Add(prop.Name!);
        }

        var stats_properties = typeof(Stats).GetProperties();
        foreach (var prop in stats_properties) {
            var instance = Expression.Parameter(typeof(Stats), "p");
            var getExpr = Expression.Property(instance, prop);
            var value = Expression.Parameter(typeof(double), "v");
            var setExpr = Expression.Assign(getExpr, value);

            stats_accessors.Add(
                (
                    Expression.Lambda<Func<Stats, double>>(getExpr, instance).Compile(),
                    Expression.Lambda<Action<Stats, double>>(setExpr, instance, value).Compile()
                )
            );
            stats_names.Add(prop.Name!);
        }
    }
}