namespace AdventOfCode2024.Solvers;
public class Day5Solver : DaySolver
{
    public override int Day => 5;
    private List<(int, int)> _rules = [];
    private List<List<int>> _updates = [];
    public override void LoadInputData(string[] input)
    {
        var splitIndex = input.ToList().FindIndex(string.IsNullOrWhiteSpace);
        _rules = input.Take(splitIndex - 1)
            .Select(s => s.Split('|'))
            .Select(s => (int.Parse(s[0]), int.Parse(s[1])))
            .ToList();

        _updates = input.TakeLast(input.Length - splitIndex - 1)
            .Select(s =>
                s.Split(',')
                    .Select(int.Parse)
                    .ToList())
            .ToList();
    }
    public override string SolvePartOne()
    {
        return _updates
            .Where(IsCorrectlyOrdered)
            .Sum(u => u[u.Count / 2])
            .ToString();
    }

    public override string SolvePartTwo()
    {
        return _updates
            .Where(u => !IsCorrectlyOrdered(u))
            .Select(OrderByRules)
            .Sum(u => u[u.Count / 2])
            .ToString();
    }
    
    private List<(int,int)> GetRulesForUpdate(List<int> update) => _rules.Where(r => update.Any(x => x == r.Item1) && update.Any(x => x == r.Item2)).ToList();

    private bool IsCorrectlyOrdered(List<int> update)
    {
        return !(from rule in GetRulesForUpdate(update)
                 let indexItem1 = update.IndexOf(rule.Item1)
                 let indexItem2 = update.IndexOf(rule.Item2)
                 where indexItem1 > indexItem2
                 select 1)
            .Any();
    }

    private List<int> OrderByRules(List<int> update)
    {
        var rules = GetRulesForUpdate(update).OrderBy(x => x.Item1);
        foreach (var rule in rules)
        {
            var indexItem1 = update.IndexOf(rule.Item1);
            var indexItem2 = update.IndexOf(rule.Item2);

            if (indexItem1 < indexItem2)
                continue;

            var item1 = update.First(x => x == rule.Item1);
            update.Remove(item1);
            update.Insert(indexItem2, item1);
        }
        
        return update;
    }
}