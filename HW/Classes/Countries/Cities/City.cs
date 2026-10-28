namespace HW.Classes;

public class City
{
    public int       Id { get; }
    public string    Name { get; }
    public Buildings Buildings { get; }
    public double    SqrKm { get; }

    public City(int id, string name, double sqrKm)
    {
        Id = id;
        Name = (name.Trim() == "") ? $"Unnamed city-{id}" : name;
        Buildings = new();
        if (sqrKm <= 0.0)
            throw new("~Nya! T-the city s-size is way t-too small!!~");
        else
            SqrKm = sqrKm;
    }
}