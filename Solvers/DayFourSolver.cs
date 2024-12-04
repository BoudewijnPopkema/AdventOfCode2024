using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solvers;
public partial class DayFourSolver : DaySolver
{
    public override int Day => 4;
    private string[]? _horizontalStrings;
    private string[]? _verticalStrings;
    private string[]? _diagonalAscendingStrings;
    private string[]? _diagonalDescendingStrings;
    public override void LoadInputData(string[] input)
    {
        var size = input.Length;
        _horizontalStrings = input;
        _verticalStrings = new string[size];
        _diagonalAscendingStrings = new string[size];
        _diagonalDescendingStrings = new string[size];

        for (var i = 0; i < size; i++)
        {
            _verticalStrings[i] = new string(input.Select(s => s[i]).ToArray());
        }

        // Get all diagonal ascending lines, starting at top left
        for (var line = 0; line < size; line++)
        {
            for (var pos = 0; pos <= line + size; pos++)
            {
                var x = Math.Min(size, pos);
                var y = line - pos;
                _diagonalAscendingStrings[line] += input[x][y];
                _diagonalDescendingStrings[line] += input[size - x - 1][y];
            }
        }
    }
    public override string SolvePartOne()
    {
        var strings = _diagonalAscendingStrings.Concat(_diagonalDescendingStrings).Concat(_verticalStrings).Concat(_horizontalStrings);
        return XmasRegex().Matches(string.Join(',',strings)).Count.ToString();
    }
    public override string SolvePartTwo()
    {
        throw new NotImplementedException();
    }

    [GeneratedRegex("XMAS|SAMX")]
    private static partial Regex XmasRegex();
}