using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solvers;

public class DayFourSolver : DaySolver
{
    public override int Day => 4;
    private char[][]? _grid;

    public override void LoadInputData(string[] input)
    {
        _grid = input.Select(x => x.ToCharArray()).ToArray();
    }

    public override string SolvePartOne()
    {
        List<char?[][]> patterns =
        [
            new char?[][] {
                ['X', 'M', 'A', 'S']
            },
            new char?[][] {
                ['S', 'A', 'M', 'X']
            },
            new char?[][] {
                ['X'],
                ['M'],
                ['A'],
                ['S'],
            },
            new char?[][] {
                ['S'],
                ['A'],
                ['M'],
                ['X'],
            },
            new char?[][] {
                ['X', ' ', ' ', ' '],
                [' ', 'M', ' ', ' '],
                [' ', ' ', 'A', ' '],
                [' ', ' ', ' ', 'S'],
            },
            new char?[][] {
                ['S', ' ', ' ', ' '],
                [' ', 'A', ' ', ' '],
                [' ', ' ', 'M', ' '],
                [' ', ' ', ' ', 'X'],
                },
            new char?[][] {
                [' ', ' ', ' ', 'S'],
                [' ', ' ', 'A', ' '],
                [' ', 'M', ' ', ' '],
                ['X', ' ', ' ', ' '],
            },
            new char?[][] {
                [' ', ' ', ' ', 'X'],
                [' ', ' ', 'M', ' '],
                [' ', 'A', ' ', ' '],
                ['S', ' ', ' ', ' '],
            },
        ];

        return CountGridPatternMatches(patterns).ToString();
    }

    public override string SolvePartTwo()
    {
        List<char?[][]> patterns =
        [
            new char?[][] {
                ['M', ' ', 'S'],
                [' ', 'A', ' '],
                ['M', ' ', 'S'],
            },
            new char?[][] {
                ['S', ' ', 'M'],
                [' ', 'A', ' '],
                ['S', ' ', 'M'],
            },
            new char?[][] {
                ['S', ' ', 'S'],
                [' ', 'A', ' '],
                ['M', ' ', 'M'],
            },
            new char?[][] {
                ['M', ' ', 'M'],
                [' ', 'A', ' '],
                ['S', ' ', 'S'],
            },
        ];

        return CountGridPatternMatches(patterns).ToString();
    }

    private int CountGridPatternMatches(IEnumerable<char?[][]> patterns) => patterns.Sum(CountGridPatternMatches);

    private int CountGridPatternMatches(char?[][] pattern)
    {
        var matches = 0;
        var patternWidth = pattern[0].Length;
        var patternHeight = pattern.Length;
        var gridSize = _grid![0].Length;
        // Loop through all pattern positions
        for (var x = 0; x <= gridSize - patternWidth; x++)
        {
            for (var y = 0; y <= gridSize - patternHeight; y++)
            {
                if (GridPositionMatchesPattern(x, y))
                    matches++;
            }
        }

        return matches;

        bool GridPositionMatchesPattern(int x, int y)
        {
            // Loop through pattern for position
            for (var pX = 0; pX < patternWidth; pX++)
            {
                for (var pY = 0; pY < patternHeight; pY++)
                {
                    var skip = pattern[pY][pX] == ' ';
                    if (!skip && _grid[y + pY][x + pX] != pattern[pY][pX])
                        return false;
                }
            }

            return true;
        }
    }
}