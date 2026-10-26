using System.ComponentModel.DataAnnotations;

namespace HW.Classes;

public class City
{
    public int       Id { get; }
    public string    Name { get; }
    public CityType  Type { get; }
    public Buildings Buildings { get; }
    public double    SqrKm { get; }

    public City(int id, string name, CityType type, Buildings buildings, double sqrKm)
    {
        Id = id;
        Name = (name.Trim() == "") ? $"Unnamed city-{id}" : name;
        Type = type;
        Buildings = buildings;
        if (sqrKm <= 0.0)
            throw new("~Nya! T-the city s-size is way t-too small!!~");
        else
            SqrKm = sqrKm;
    }
}