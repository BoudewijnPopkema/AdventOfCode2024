using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solvers;

public class DayThreeSolver : DaySolver
{
    public override int Day => 3;
    private MatchCollection? _matchCollection;

    public override void LoadInputData(string[] input)
    {
        _matchCollection = new Regex(@"(?'mul'mul\(\d{1,3},\d{1,3}\))|(?'do'do\(\))|(?'dont'don't\(\))").Matches(string.Concat(input));
    }

    public override string SolvePartOne()
    {
        return _matchCollection!
            .Where(x => GetGroupName(x) == "mul")
            .Sum(x => GetMultiplicationValue(x.Value)).ToString();
    }

    public override string SolvePartTwo()
    {
        var enabled = true;
        return _matchCollection!.Sum(match =>
        {
            switch (GetGroupName(match))
            {
                case "mul":
                    return enabled ? GetMultiplicationValue(match.Value) : 0;
                case "do":
                    enabled = true;
                    return 0;
                case "dont":
                    enabled = false;
                    return 0;
                default: return 0;
            }
        }).ToString();
    }

    private static string GetGroupName(Match m) => m.Groups.Values.First(g => g.Success && g.GetType() == typeof(Group)).Name;

    private static int GetMultiplicationValue(string s)
    {
        var values =
            s.Replace("mul(", "")
                .Replace(")", "")
                .Split(",")
                .Select(int.Parse).ToArray();
        return values[0] * values[1];
    }
}