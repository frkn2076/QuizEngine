namespace Quiz.Engine.Helper;

public static class FormatHelper
{
    public static double FormatToDecimalPlaces(double value, int numberOfDecimalPlaces)
    {
        var multiplier = Math.Pow(10, numberOfDecimalPlaces);
        return Math.Floor(value * multiplier) / multiplier;
    }

    public static string FormatToPercentage(double value)
    {
        var formattedValue = FormatToDecimalPlaces(value, 2);
        return $"{(int)(formattedValue * 100)}%";
    }

    public static double CalculateWeight(int count)
    {
        return Math.Round(1.0 / count, 3);
    }
}
