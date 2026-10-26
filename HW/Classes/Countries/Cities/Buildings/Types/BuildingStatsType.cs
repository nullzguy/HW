namespace HW.Classes;

public class BuildingStatsType
{
    public BuildingType Type { get; }
    public double HappinessPerM2 { get; }
    public double GDPPerM2 { get; }
    public double FoodPerM2 { get; }
    public double DefaultAreaM2 { get; }

    public BuildingStatsType(BuildingType type, double happy, double gdp, double food, double defaultArea)
    {
        Type = type;
        HappinessPerM2 = happy;
        GDPPerM2 = gdp;
        FoodPerM2 = food;
        DefaultAreaM2 = defaultArea;
    }
}