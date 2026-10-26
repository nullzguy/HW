namespace HW.Classes;

abstract class Country
{
    public int Id { get; }
    public string Name { get; }
    public GovType GovernmentType { get; }
    public Resources ResourcesList { get; }
    public Cities CitiesList { get; }
    public Decimal GDP { get; }

    protected Country(int id, GovType governmentType, string name)
    {
        Id = id;
        Name = (name.Trim() == "") ? $"Unnamed country-{id}" : name;
        GovernmentType = governmentType;
        ResourcesList = new Resources();
        CitiesList = new Cities();
        GDP = ;
    }
}