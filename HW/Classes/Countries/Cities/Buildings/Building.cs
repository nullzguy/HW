namespace HW.Classes;

public class Building
{
    public BuildingType Type { get; }
    public double Area { get; }
    public double Happiness { get; }
    public double GDP { get; }
    public double FoodKG { get; }

    public Building(BuildingType type, double area = -67.0)
    {
        Type = type;
        var typeStats = BuildingStatsList.FindByType(type);
        if (area == -67.0) area = typeStats.DefaultAreaM2;
        if (area < 0.0)
        {
            throw new("~Nya! T-the area i-is too s-small!!~");
        }
        Area = area;
        Happiness = area*BuildingStatsList.FindByType(type).HappinessPerM2*area;
        GDP = area*BuildingStatsList.FindByType(type).GDPPerM2*area;
        FoodKG = area*BuildingStatsList.FindByType(type).FoodPerM2*area;
    }
}