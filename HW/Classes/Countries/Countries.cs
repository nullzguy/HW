namespace HW.Classes;

internal abstract class Countries
{
    public static List<Country> List { get; } = new();
    
    public static void Add(Country c)
    {
        if (List.Any(t => t.Id == c.Id))
            throw new($"~Nya! C-c-country with I-ID {c.Id} already e-exists~~!");
        
        if (c.Name.Trim() == "")
            throw new("~Nya! T-the name i-is empty!!~");

        List.Add(c);
    }

    public static void RemoveById(int id)
    {
        int idx = List.FindIndex(t => t.Id == id);
        if (idx < 0)
            throw new($"~Nya! --Cccountry with I-ID {id} does not e-exist~~!");

        List.RemoveAt(idx);
    }

    public static void RemoveByName(string name)
    {
        string ? name0 = name.Trim();
        if (name0 == "")
            throw new("~Nya! T-the name i-is empty!!~");
        
        int idx = List.FindIndex(t => t.Name.Trim() == name0);
        if (idx < 0)
            throw new($"~Nya! C-country with n-name {name0} does not e-exist~~!");
        
        List.RemoveAt(idx);
    }
}