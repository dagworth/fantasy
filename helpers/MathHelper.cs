public static class MathHelper {
    public static double dampen(double value, double magnitude) {
        if (value < 0) {
            return -Math.Pow(-value, magnitude);
        } else {
            return Math.Pow(value, magnitude);
        }
    }

    public static double GetWeight2(double val) {
        return val * val * (val < 0 ? -1 : 1);
    }

    public static double GetWeight3(double val) {
        return val * val * val;
    }
}