namespace HW.Classes;

public class Building
{
    public BuildingType Type { get; }
    public double Area { get; }
    public double Happiness { get; }
    public double GDP { get; }
    public double FoodKG { get; }

    public Building(BuildingType type, double area = BuildingStatsList.List)
    {
        Type = type;
        if (area < 0.0)
        {
            throw new("~Nya! T-the area i-is too s-small!!~");
        }
        Area = area;
        Happiness = area*;
    }
}