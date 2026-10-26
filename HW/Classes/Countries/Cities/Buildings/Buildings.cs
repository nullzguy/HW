namespace HW.Classes;

public class Buildings
{
    public List<Building> List { get; }
    public double Happiness { get; }
    public double GDP { get; }
    public double Food { get; }
    public double Taxes { get; }
    public double M2 { get; }

    public Buildings()
    {
        List = new();
        Happiness = List.Sum(h => h.Happiness);
        GDP = List.Sum(g => g.GDP);
    }
}