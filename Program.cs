using System.Globalization;
using CaixaDespesas.Extensions;
using CaixaDespesas.Models;

namespace CaixaDespesas
{
    class Program
    {
        const char InfoSeparator = ';';
        const char FilterSeparator = '|';

        static void Main(string[] args)
        {
            String path;
            String[] filter;
            bool group;

            if (!ArgumentsValid(args, out path, out filter, out group))
                throw new ArgumentException();

            if (!File.Exists(path))
                throw new FileNotFoundException();

            var movements = ReadMovementsFile(path, filter);

            movements = Group(group, movements);
            PrintMovementsByType(movements, MovementType.Debit, group);
            PrintMovementsByType(movements, MovementType.Credit, group);
        }

        static List<Movement> ReadMovementsFile(String path, String[] filter)
        {
            var movements = new List<Movement>();

            var fileLines = File.ReadAllLines(path);

            foreach (var line in fileLines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var splitLine = line.Split(InfoSeparator);

                if (splitLine.Length != 4) continue;

                var description = splitLine[1];
                var movementType = string.IsNullOrWhiteSpace(splitLine[2]) ? MovementType.Credit : MovementType.Debit;

                if (filter.Any() && !filter.Any(f => description.ToLower().Contains(f)))
                    continue;

                movements.Add(new Movement
                {
                    Date = DateTime.ParseExact(splitLine[0], "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    Description = description,
                    Type = movementType,
                    Ammount = movementType == MovementType.Debit ? splitLine[2].ToDecimal() : splitLine[3].ToDecimal()
                });
            }

            return movements;
        }

        static List<Movement> Group(bool group, List<Movement> movements)
        {
            if(!group) return movements;

            var groupedMovements = new List<Movement>();
            var groupByDescription = movements.GroupBy(x => new { x.Description, x.Type });

            foreach (var grouPair in groupByDescription)
            {
                var key = grouPair.FirstOrDefault() ?? new Movement();

                groupedMovements.Add(new Movement
                {
                    Ammount = grouPair.Sum(x => x.Ammount),
                    Date = key.Date,
                    Description = key.Description,
                    Type = key.Type
                });
            }

            return groupedMovements;
        }

        static bool ArgumentsValid(string[] args, out String path, out String[] filter, out bool group)
        {
            path = string.Empty;
            filter = new string[0];
            group = true;

            if (!args.Any()) return false;

            path = args[0];

            if (args.Length > 1) filter = args[1].Split(FilterSeparator).Select(x => x.ToLower()).ToArray();
            if (args.Length > 2)
            {
                if (!bool.TryParse(args[2], out group))
                    return false;
            }

            return true;
        }

        static void PrintMovementsByType(List<Movement> movements, MovementType type, bool group)
        {
            if(!movements.Any(x => x.Type == type)) return;

            Console.ForegroundColor = type == MovementType.Credit ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"\n{(type == MovementType.Credit ? "RECEITAS" : "DESPESAS")}:");

            foreach (var movement in movements.Where(x => x.Type == type))
                Console.WriteLine(movement.ToDetail(!group));

            Console.WriteLine($"TOTAL: {GetTotalByType(movements, type)}€");
        }

        static decimal GetTotalByType(List<Movement> movements, MovementType type)
        {
            return movements.Where(x => x.Type == type).Sum(x => x.Ammount);
        }
    }
}