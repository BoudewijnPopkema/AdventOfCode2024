using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solvers;

public partial class DayThreeSolver : DaySolver
{
    private List<Tuple<int, int>> _multiplications = [];
    private string _input = string.Empty;

    public override void LoadInputData(string[] input)
    {
        _input = string.Concat(input);
        var matches = MultiplicationRegex().Matches(_input);
        _multiplications =
            matches.Select(s =>
                    s.Value.Replace("mul(", "")
                        .Replace(")", "")
                        .Split(",")
                ).Select(a => new Tuple<int, int>(int.Parse(a[0]), int.Parse(a[1])))
                .ToList();
    }

    public override string SolvePartOne()
    {
        return _multiplications.Sum(m => m.Item1 * m.Item2).ToString();
    }

    public override string SolvePartTwo()
    {
        while (true)
        {
            var dont = DontRegex().Match(_input);
            if (!dont.Success) break;
            
            var nextDo = DoRegex().Match(_input[dont.Index..]);
            if (!nextDo.Success)
            {
                _input = _input[..dont.Index];
                break;
            }
            _input = _input.Remove(dont.Index, nextDo.Index);
        }

        LoadInputData([_input]);
        return SolvePartOne();
    }

    public override int Day => 3;

    [GeneratedRegex(@"mul\(\d{1,3},\d{1,3}\)")]
    private static partial Regex MultiplicationRegex();

    [GeneratedRegex(@"do\(\)")]
    private static partial Regex DoRegex();

    [GeneratedRegex(@"don't\(\)")]
    private static partial Regex DontRegex();
}