using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solvers;

public partial class DayFourSolver : DaySolver
{
    public override int Day => 4;
    private char[][]? _grid;
    private int _size;

    public override void LoadInputData(string[] input)
    {
        _grid = input.Select(x => x.ToCharArray()).ToArray();
        _size = _grid[0].Length;
    }

    public override string SolvePartOne()
    {
        var diagonals = _size * 2 - 1;
        var horizontalStrings = new string[_size];
        var verticalStrings = new string[_size];
        for (var x = 0; x < _size; x++)
        {
            for (var y = 0; y < _size; y++)
            {
                verticalStrings[x] += _grid[x][y];
                horizontalStrings[y] += _grid[x][y];
            }
        }

        var diagonalAscendingStrings = new string[diagonals];
        var diagonalDescendingStrings = new string[diagonals];

        // Get all diagonal ascending lines, starting at top left
        for (var line = 0; line < diagonals; line++)
        {
            var startX = line < _size ? 0 : line - _size + 1;
            var startY = line < _size ? line : _size - 1;
            for (var pos = 0; pos <= line; pos++)
            {
                var x = startX + pos;
                var y = startY - pos;

                if (y < 0 || x >= _size) break;

                diagonalAscendingStrings[line] += _grid[x][y];
                diagonalDescendingStrings[line] += _grid[_size - x - 1][y];
            }
        }

        var strings = diagonalAscendingStrings.Concat(diagonalDescendingStrings).Concat(verticalStrings).Concat(horizontalStrings);
        var joinedStrings = string.Join(' ', strings);
        return (XmasRegex().Matches(joinedStrings).Count + SamxRegex().Matches(joinedStrings).Count).ToString();
    }

    public override string SolvePartTwo()
    {
        var matches = 0;
        for (var x = 1; x < _size - 1; x++)
        {
            for (var y = 1; y < _size - 1; y++)
            {
                if (GridMatchesXMAS(x, y))
                    matches++;
            }
        }

        return matches.ToString();
    }

    private bool GridMatchesXMAS(int x, int y)
    {
        var tl = _grid![x - 1][y + 1];
        var tr = _grid[x + 1][y + 1];
        var bl = _grid[x - 1][y - 1];
        var br = _grid[x + 1][y - 1];

        return _grid[x][y] == 'A'
               && (tl == 'S' && tr == 'S' && bl == 'M' && br == 'M'
                   || tl == 'S' && tr == 'M' && bl == 'S' && br == 'M'
                   || tl == 'M' && tr == 'M' && bl == 'S' && br == 'S'
                   || tl == 'M' && tr == 'S' && bl == 'M' && br == 'S');
    }

    [GeneratedRegex("XMAS")]
    private static partial Regex XmasRegex();

    [GeneratedRegex("SAMX")]
    private static partial Regex SamxRegex();
}