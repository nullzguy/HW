namespace HW.Classes;

public class Buildings
{
    public List<Building> List { get; }
    public double Happiness => List.Sum(h => h.Happiness);
    public double GDP => List.Sum(g => g.GDP);
    public double Food => List.Sum(f => f.FoodKG);
    public double M2 => List.Sum(t => t.Area);

    public Buildings()
    {
        List = new();
    }

    public void Add(Building b)
    {
        List.Add(b);
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= List.Count)
            throw new($"~Nya! B-building at i-index {index} does not e-exist~~!");

        List.RemoveAt(index);
    }
}