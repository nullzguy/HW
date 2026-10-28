namespace HW.Classes;

public class Cities
{
    public List<City> List { get; }
    public double TotalHappiness => List.Sum(h => h.Buildings.Happiness);
    public double TotalGDP => List.Sum(g => g.Buildings.GDP);
    public double TotalFoodKg => List.Sum(f => f.Buildings.Food);
    public double TotalArea => List.Sum(a => a.SqrKm);

    public Cities()
    {
        List = new();
    }
    
    public void Add(City c)
    {
        if (List.Any(t => t.Id == c.Id))
            throw new($"~Nya! C-c-city with I-ID {c.Id} already e-exists~~!");
        
        if (c.Name.Trim() == "")
            throw new("~Nya! T-the name i-is empty!!~");

        List.Add(c);
    }

    public void RemoveById(int id)
    {
        int idx = List.FindIndex(t => t.Id == id);
        if (idx < 0)
            throw new($"~Nya! --Cccity with I-ID {id} does not e-exist~~!");

        List.RemoveAt(idx);
    }

    public void RemoveByName(string name)
    {
        string ? name0 = name.Trim();
        if (name0 == "")
            throw new("~Nya! T-the name i-is empty!!~");
        
        int idx = List.FindIndex(t => t.Name.Trim() == name0);
        if (idx < 0)
            throw new($"~Nya! C-city with n-name {name0} does not e-exist~~!");
        
        List.RemoveAt(idx);
    }
}