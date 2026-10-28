namespace HW.Classes;

abstract class Country
{
    public int Id { get; }
    public string Name { get; }
    public GovType GovernmentType { get; }
    public Resources ResourcesList { get; }
    public Cities CitiesList { get; }
    public long Population { get; set; }
    public double Happiness { get; set; }
    public double Food { get; set; }
    public double GDP { get; set; }
    public double Area { get; set; }

    protected Country(int id, string name, GovType governmentType, double area)
    {
        Id = id;
        Name = (name.Trim() == "") ? $"Unnamed country-{id}" : name;
        GovernmentType = governmentType;
        ResourcesList = new Resources();
        CitiesList = new Cities();
        GDP = 0;
        Area = area;
    }

    public void Step()
    {
        GovType government = GovTypes.FindByName(GovernmentType.Name);

        GDP += (
            (CitiesList.TotalGDP + ResourcesList.List.Sum(g=>g.Income)) *
            Math.Exp(government.Control)
        );

        Food += CitiesList.TotalFoodKg;

        // Сорян, але так, я нейронку використовував для ідей формул ось тут.
        double population = Math.Max(Population, 1.0);
        double foodRequired = population;
        double foodAvailable = Math.Max(Food, 0.0);
        double foodConsumed = Math.Min(foodAvailable, foodRequired);
        Food = foodAvailable - foodConsumed;
        double foodSecurity = foodConsumed / foodRequired;

        Happiness = (CitiesList.TotalHappiness + government.HappinessBonus*population) *
                    (0.5 + 0.5*foodSecurity);

        double j = 1000.0 * population;
        double t = Math.Clamp(Food/j, 0.0, 1.0);
        double m = 1.0 + 0.1 * Math.Tanh(Happiness/(0.1*j)) * Math.Sqrt(t);
        m -= (1.0 - foodSecurity) * 0.1;
        m = Math.Clamp(m, 0.5, 1.5);

        Population = Math.Max(1L, (long)Math.Round(Population*m));
    }
}

class StandardCountry : Country
{
    public StandardCountry(int id, string name, GovType governmentType, double area)
        : base(id, name, governmentType, area)
    {
    }
}