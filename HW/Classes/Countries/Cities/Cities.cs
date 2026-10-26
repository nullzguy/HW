namespace HW.Classes;

public class Cities
{
    private List<City> List { get; }
    public double TotalArea { get; }

    public Cities()
    {
        List = new();
        TotalArea = List.Sum(a => a.SqrKm);
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