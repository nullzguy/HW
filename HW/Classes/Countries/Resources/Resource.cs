namespace HW.Classes;

public class Resource
{
    public int    Id { get; }
    public string Name { get; }
    public double PricePerKg { get; }
    public double MPerKm { get; }
    // метры^3 материала на километр^3 земли
    public double Income { get; }

    public Resource(int id, string name, double pricePerKg, double mPerKm)
    {
        Id = id;
        Name = (name.Trim() == "") ? $"Unnamed material-{id}" : name;
        PricePerKg = pricePerKg;
        if (mPerKm <= 0.0)
            throw new("~Nya! T-the r-richness is way t-too small!!~");
        MPerKm = mPerKm;
        Income = mPerKm * pricePerKg - 1.0/mPerKm;
        // 1.0/mPerKm типо как потраченные ресурсы на добычу
    }
}














// ня