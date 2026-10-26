namespace HW.Classes;

public class GovType
{
    public int    Id { get; }
    public string Name { get; }
    public double HappinessBonus { get; }
    public double Control { get; }

    public GovType(int id, string name, double happinessBonus, double control)
    {
        Id = id;
        Name = (name.Trim() == "") ? $"Unnamed government type-{id}" : name;
        HappinessBonus = happinessBonus;
        Control = control;
    }
}