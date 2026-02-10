using System.Linq.Expressions;
using System.Reflection;

public static class FastCompile {
    public static List<(Func<Perception, double> Get, Action<Perception, double> Set)> perception_accessors = new();

    public static void init(PropertyInfo[] properties) {
        foreach (var prop in properties) {
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
        }
    }
}