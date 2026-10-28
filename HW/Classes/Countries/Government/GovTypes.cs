namespace HW.Classes;

public static class GovTypes
{
    readonly public static List<GovType> List = new()
    {
        new GovType(0, "Capitalism", 0.5, -0.3),
        new GovType(1, "Communism", -0.3, 0.8),
        new GovType(2, "Monarchy", 0.0, 0.6),
        new GovType(3, "Democracy", 0.7, 0.2),
        new GovType(4, "Globalism", 0.6, 0.0)
    };

    public static void Add(GovType g)
    {
        if (List.Any(t => t.Id == g.Id))
            throw new($"~Nya! Governm-m-ment with I-ID {g.Id} already e-exists~~!");
        
        if (List.Any(t => t.Name == g.Name))
            throw new($"~Nya! Governm-m-ment with n-name {g.Name} already e-exists~~!");

        if (g.Name.Trim() == "")
            throw new("~Nya! T-the name i-is empty!!~");

        List.Add(g);
    }

    public static void RemoveById(int id)
    {
        int idx = List.FindIndex(t => t.Id == id);
        if (idx < 0)
            throw new($"~Nya! Governm-m-ment with I-ID {id} does not e-exist~~!");

        List.RemoveAt(idx);
    }

    public static void RemoveByName(string name)
    {
        string ? name0 = name.Trim();
        if (name0 == "")
            throw new("~Nya! T-the name i-is empty!!~");
        
        int idx = List.FindIndex(t => t.Name.Trim() == name0);
        if (idx < 0)
            throw new($"~Nya! Governm-m-ment with n-name {name0} does not e-exist~~!");
        
        List.RemoveAt(idx);
    }
    
    public static GovType FindByName(string name)
    {
        string ? name0 = name.Trim();
        if (name0 == "")
            throw new("~Nya! T-the name i-is empty!!~");
        return List.Find(stats => stats.Name == name) ?? 
               throw new ($"~Nya! Governm-m-ment with n-name {name0} does not e-exist~~!");
    }
}