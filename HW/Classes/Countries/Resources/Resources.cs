namespace HW.Classes;

public class Resources
{
    public List<Resource> List { get; }

    public Resources()
    {
        List = new();
    }
    
    public void Add(Resource r)
    {
        if (List.Any(t => t.Id == r.Id))
            throw new($"~Nya! R-r-resource with I-ID {r.Id} already e-exists~~!");
        
        if (r.Name.Trim() == "")
            throw new("~Nya! T-the name i-is empty!!~");

        List.Add(r);
    }

    public void RemoveById(int id)
    {
        int idx = List.FindIndex(t => t.Id == id);
        if (idx < 0)
            throw new($"~Nya! --Rrresource with I-ID {id} does not e-exist~~!");

        List.RemoveAt(idx);
    }

    public void RemoveByName(string name)
    {
        string ? name0 = name.Trim();
        if (name0 == "")
            throw new("~Nya! T-the name i-is empty!!~");
        
        int idx = List.FindIndex(t => t.Name.Trim() == name0);
        if (idx < 0)
            throw new($"~Nya! R-resource with n-name {name0} does not e-exist~~!");
        
        List.RemoveAt(idx);
    }
}