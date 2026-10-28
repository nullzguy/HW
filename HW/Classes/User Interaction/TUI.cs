using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Xml;

namespace HW.Classes;

public class TUI
{
    public static void Run()
    {
        int ? width =  Console.WindowWidth;
        int ? height = Console.WindowHeight;

        Console.WriteLine($" -< Nullz's country economics simulator >-");
        Console.WriteLine($"'h' for a list of commands\n" +
                          $"'q' to quit");

        bool q = false;
        while (q == false)
        {
            Console.Write(" > ");
            string ? input = Console.ReadLine();
            try
            {
                var args = TokenizeInput(input);
                RunCommand(args, ref q);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                continue;
            }

            //RunCommand(args);
        }
    }

    private static string[] TokenizeInput(string input)
    {
        var input0 = input.Trim();
        if (input0 == "")
            throw new("~Nya! T-the input i-is empty!!~");
        return input0.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }

    private static void RunCommand(string[] args, ref bool q)
    {
        // Help
        if (args[0] == "h" || args[0] == "help")
        {
            if (args.Length == 1)
            {
                RunHelp();
                return;
            }
            else
                throw new("~Nya! T-too many a-args!!~");
        }

        // Quit
        if (args[0] == "q" || args[0] == "quit")
        {
            if (args.Length == 1)
            {
                q = true;
                return;
            }
            else
                throw new("~Nya! T-too many a-args!!~");
        }

        // Clear
        if (args[0] == "clear")
        {
            if (args.Length == 1)
            {
                Console.Clear();
                return;
            }
            else
                throw new("~Nya! T-too many a-args!!~");
        }

        // Country
        if (args[0] == "country")
        {
            RunCountry(args);
            return;
        }
        
        // Unknown command exception~~
        throw new("~Nya! U-unknown command!!~\n" +
                  "T-try entering 'h'..!~");
    }

    private static void RunCountry(string[] args)
    {
        if (args.Length < 2)
            throw new("~Nya! E-expected more a-args!!~");

        switch (args[1])
        {
            case "list":
                Expect(args, 2);
                ListCountries();
                return;
            case "add":
                AddCountry(args);
                return;
            case "remove":
                Expect(args, 3);
                RemoveCountry(args[2]);
                return;
            case "info":
                Expect(args, 3);
                ShowCountry(FindCountry(args[2]));
                return;
            case "step":
                Expect(args, 3);
                StepCountry(args[2]);
                return;
            case "cities":
                Expect(args, 3);
                ListCities(FindCountry(args[2]));
                return;
            case "city":
                RunCountryCity(args);
                return;
            case "buildings":
                Expect(args, 4);
                Country buildingsCountry = FindCountry(args[2]);
                ListBuildings(buildingsCountry, FindCity(buildingsCountry, args[3]));
                return;
            case "building":
                RunCountryBuilding(args);
                return;
            case "resources":
                Expect(args, 3);
                ListResources(FindCountry(args[2]));
                return;
            case "resource":
                RunCountryResource(args);
                return;
            default:
                throw new("~Nya! U-unknown country command!!~\n" +
                          "T-try entering 'h'..!~");
        }
    }

    private static void AddCountry(string[] args)
    {
        if (args.Length < 4 || args.Length > 5)
            throw new("~Nya! E-expected: country add <Name> <GovType> [Area]!!~");

        string name = args[2];
        GovType government = GovTypes.FindByName(args[3]);
        double area = args.Length == 5 ? double.Parse(args[4]) : 1000.0;
        int id = Countries.List.Count;

        Countries.Add(new StandardCountry(id, name, government, area));
        Console.WriteLine($"Added country {id}: {name}");
    }

    private static void RemoveCountry(string key)
    {
        if (int.TryParse(key, out int id))
            Countries.RemoveById(id);
        else
            Countries.RemoveByName(key);

        Console.WriteLine($"Removed country {key}.");
    }

    private static void StepCountry(string key)
    {
        if (key == "all")
        {
            if (Countries.List.Count == 0)
            {
                Console.WriteLine("Whoopsies, no countries are present yet.");
                return;
            }

            foreach (Country country in Countries.List)
                country.Step();

            Console.WriteLine($"Stepped {Countries.List.Count} countries.");
            return;
        }

        Country c = FindCountry(key);
        c.Step();
        ShowCountry(c);
    }

    private static void RunCountryCity(string[] args)
    {
        if (args.Length < 4)
            throw new("~Nya! E-expected more a-args!!~");

        switch (args[2])
        {
            case "add":
                if (args.Length != 6)
                    throw new("~Nya! E-expected: country city add <Country> <Name> <SqrKm>!!~");

                Country addCountry = FindCountry(args[3]);
                int id = addCountry.CitiesList.List.Count;
                addCountry.CitiesList.Add(new City(id, args[4], double.Parse(args[5])));
                Console.WriteLine($"Added city {id}: {args[4]}");
                return;
            case "remove":
                if (args.Length != 5)
                    throw new("~Nya! E-expected: country city remove <Country> <Id|Name>!!~");

                RemoveCity(FindCountry(args[3]), args[4]);
                return;
            default:
                throw new("~Nya! U-unknown city command!!~");
        }
    }

    private static void RemoveCity(Country country, string key)
    {
        if (int.TryParse(key, out int id))
            country.CitiesList.RemoveById(id);
        else
            country.CitiesList.RemoveByName(key);

        Console.WriteLine($"Removed city {key} from {country.Name}.");
    }

    private static void RunCountryBuilding(string[] args)
    {
        if (args.Length < 4)
            throw new("~Nya! E-expected more a-args!!~");

        switch (args[2])
        {
            case "add":
                if (args.Length < 6 || args.Length > 7)
                    throw new("~Nya! E-expected: country building add <Country> <City> <Type> [AreaM2]!!~");

                Country addCountry = FindCountry(args[3]);
                City addCity = FindCity(addCountry, args[4]);
                if (!Enum.TryParse(args[5], true, out BuildingType type))
                    throw new($"~Nya! B-building type {args[5]} does not e-exist~~!");

                Building building = args.Length == 7
                    ? new Building(type, double.Parse(args[6]))
                    : new Building(type);
                addCity.Buildings.Add(building);
                Console.WriteLine($"Added {type} to {addCity.Name}.");
                return;
            case "remove":
                if (args.Length != 6)
                    throw new("~Nya! E-expected: country building remove <Country> <City> <Index>!!~");

                Country removeCountry = FindCountry(args[3]);
                City removeCity = FindCity(removeCountry, args[4]);
                removeCity.Buildings.RemoveAt(int.Parse(args[5]));
                Console.WriteLine($"Removed building from {removeCity.Name}.");
                return;
            default:
                throw new("~Nya! U-unknown building command!!~");
        }
    }

    private static void RunCountryResource(string[] args)
    {
        if (args.Length < 4)
            throw new("~Nya! E-expected more a-args!!~");

        switch (args[2])
        {
            case "add":
                if (args.Length != 7)
                    throw new("~Nya! E-expected: country resource add <Country> <Name> <PricePerKg> <MPerKm>!!~");

                Country addCountry = FindCountry(args[3]);
                int id = addCountry.ResourcesList.List.Count;
                addCountry.ResourcesList.Add(new Resource(
                    id, args[4], double.Parse(args[5]), double.Parse(args[6])));
                Console.WriteLine($"Added resource {id}: {args[4]}");
                return;
            case "remove":
                if (args.Length != 5)
                    throw new("~Nya! E-expected: country resource remove <Country> <Id|Name>!!~");

                RemoveResource(FindCountry(args[3]), args[4]);
                return;
            default:
                throw new("~Nya! U-unknown resource command!!~");
        }
    }

    private static void RemoveResource(Country country, string key)
    {
        if (int.TryParse(key, out int id))
            country.ResourcesList.RemoveById(id);
        else
            country.ResourcesList.RemoveByName(key);

        Console.WriteLine($"Removed resource {key} from {country.Name}.");
    }

    private static Country FindCountry(string key)
    {
        Country? country = int.TryParse(key, out int id)
            ? Countries.List.Find(c => c.Id == id)
            : Countries.List.Find(c => c.Name.Trim() == key);

        return country ?? throw new($"~Nya! C-country {key} does not e-exist~~!");
    }

    private static City FindCity(Country country, string key)
    {
        City? city = int.TryParse(key, out int id)
            ? country.CitiesList.List.Find(c => c.Id == id)
            : country.CitiesList.List.Find(c => c.Name.Trim() == key);

        return city ?? throw new($"~Nya! C-city {key} does not e-exist in {country.Name}~~!");
    }

    private static void Expect(string[] args, int count)
    {
        if (args.Length != count)
            throw new("~Nya! E-expected more a-args!!~");
    }

    private static void RunHelp()
    {
        Console.WriteLine("'h' or 'help' to list all commands (this text)\n" +
                          "'q' or 'quit' to quit the program\n" +
                          "'clear' to clear all text\n" +
                          "'country list' to list countries\n" +
                          "'country add <Name> <GovType> [Area]' to add a country\n" +
                          "'country remove <Id|Name>' to remove a country\n" +
                          "'country info <Id|Name>' to show country details\n" +
                          "'country step <Id|Name|all>' to advance one step\n" +
                          "'country cities <Id|Name>' to list a country's cities\n" +
                          "'country city add <Country> <Name> <SqrKm>' to add a city\n" +
                          "'country city remove <Country> <Id|Name>' to remove a city\n" +
                          "'country buildings <Country> <City>' to list a city's buildings\n" +
                          "'country building add <Country> <City> <Type> [AreaM2]' to add a building\n" +
                          "'country building remove <Country> <City> <Index>' to remove a building\n" +
                          "'country resources <Id|Name>' to list a country's resources\n" +
                          "'country resource add <Country> <Name> <PricePerKg> <MPerKm>' to add a resource\n" +
                          "'country resource remove <Country> <Id|Name>' to remove a resource");
    }

    private static void ListCountries()
    {
        if (Countries.List.Count == 0)
        {
            Console.WriteLine("Whoopsies, no countries are present yet.\nTry adding one by using 'country add <Name> <GovType>',\nand then changing it's parameters.");
            return;
        }

        Console.WriteLine("ID | Name | Government | Population | Happiness | Food (kg) | GDP | Area (km²)");
        foreach (Country country in Countries.List)
        {
            Console.WriteLine($"{country.Id} | {country.Name} | {country.GovernmentType.Name} | " +
                              $"{country.Population} | {country.Happiness:F2} | {country.Food:F2} | " +
                              $"{country.GDP:F2} | {country.Area:F2}");
        }
    }

    private static void ShowCountry(Country country)
    {
        Console.WriteLine($"{country.Id}: {country.Name}");
        Console.WriteLine($"  Government: {country.GovernmentType.Name} " +
                          $"(happiness bonus {country.GovernmentType.HappinessBonus}, control {country.GovernmentType.Control})");
        Console.WriteLine($"  Population: {country.Population}");
        Console.WriteLine($"  Happiness: {country.Happiness:F2}");
        Console.WriteLine($"  Food: {country.Food:F2} kg");
        Console.WriteLine($"  GDP: {country.GDP:F2}");
        Console.WriteLine($"  Area: {country.Area:F2} km²");
        Console.WriteLine($"  Cities: {country.CitiesList.List.Count}");
        Console.WriteLine($"  Resources: {country.ResourcesList.List.Count}");
    }

    private static void ListCities(Country country)
    {
        if (country.CitiesList.List.Count == 0)
        {
            Console.WriteLine($"Whoopsies, {country.Name} has no cities yet.\nTry 'country city add <Country> <Name> <SqrKm>'.");
            return;
        }

        Console.WriteLine("ID | Name | Area (km²) | Buildings | Happiness | Food (kg) | GDP");
        foreach (City city in country.CitiesList.List)
        {
            Console.WriteLine($"{city.Id} | {city.Name} | {city.SqrKm:F2} | {city.Buildings.List.Count} | " +
                              $"{city.Buildings.Happiness:F2} | {city.Buildings.Food:F2} | {city.Buildings.GDP:F2}");
        }
    }

    private static void ListBuildings(Country country, City city)
    {
        if (city.Buildings.List.Count == 0)
        {
            Console.WriteLine($"Whoopsies, {city.Name} has no buildings yet.\nTry 'country building add <Country> <City> <Type> [AreaM2]'.");
            return;
        }

        Console.WriteLine("Index | Type | Area (m²) | Happiness | GDP | Food (kg)");
        for (int i = 0; i < city.Buildings.List.Count; i++)
        {
            Building building = city.Buildings.List[i];
            Console.WriteLine($"{i} | {building.Type} | {building.Area:F2} | {building.Happiness:F2} | " +
                              $"{building.GDP:F2} | {building.FoodKG:F2}");
        }
    }

    private static void ListResources(Country country)
    {
        if (country.ResourcesList.List.Count == 0)
        {
            Console.WriteLine($"Whoopsies, {country.Name} has no resources yet.\nTry 'country resource add <Country> <Name> <PricePerKg> <MPerKm>'.");
            return;
        }

        Console.WriteLine("ID | Name | Price/kg | m^3/km^3 | Income");
        foreach (Resource resource in country.ResourcesList.List)
        {
            Console.WriteLine($"{resource.Id} | {resource.Name} | {resource.PricePerKg:F2} | " +
                              $"{resource.MPerKm:F2} | {resource.Income:F2}");
        }
    }
}
