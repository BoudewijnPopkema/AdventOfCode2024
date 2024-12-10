namespace AdventOfCode2024.Solvers;

public class Day6Solver : DaySolver
{
    public override int Day => 6;
    private bool[][]? _grid;
    private (int, int)? _startingPos;
    private int _gridSize;
    private bool WithinGrid(dynamic pos) => pos.X >= 0 && pos.X < _gridSize && pos.Y >= 0 && pos.Y < _gridSize;

    public override void LoadInputData(string[] input)
    {
        _gridSize = input.Length;
        _grid = new bool[_gridSize][];
        for (var y = 0; y < _gridSize; y++)
        {
            _grid[y] = new bool[_gridSize];
            for (var x = 0; x < _gridSize; x++)
            {
                var character = input[y][x];
                if (character == '#')
                {
                    _grid[y][x] = true;
                }
                else if (character == '^')
                {
                    _startingPos = (x, y);
                }
            }
        }
    }

    public override string SolvePartOne()
    {
        return GetVisitedPositions().Count.ToString();
    }

    public override string SolvePartTwo()
    {
        var visitedPositions = GetVisitedPositions();
        var possibleBlockCount = 0;
        var positionsChecked = 0;
        foreach (var position in visitedPositions.Skip(1))
        {
            _grid![position.Item2][position.Item1] = true;

            var loops = GetVisitedPositions().Count == 0;

            if (loops)
                possibleBlockCount++;

            _grid![position.Item2][position.Item1] = false;
            
            positionsChecked++;
            Console.WriteLine(positionsChecked + "/" + 5461);
        }

        return possibleBlockCount.ToString();
    }

    private HashSet<(int, int)> GetVisitedPositions()
    {
        var guardPos = new { X = _startingPos!.Value.Item1, Y = _startingPos.Value.Item2 };
        var direction = Direction.Up;
        List<(int, int)> visitedPositions = [_startingPos.Value];

        while (WithinGrid(guardPos))
        {
            var nextPosition = new { X = guardPos.X + direction[0], Y = guardPos.Y + direction[1] };

            if (!WithinGrid(nextPosition))
            {
                break;
            }

            if (_grid![nextPosition.Y][nextPosition.X])
            {
                direction = Rotate(direction);
                continue;
            }

            guardPos = nextPosition;
            var guardPosTuple = new ValueTuple<int, int>(guardPos.X, guardPos.Y);
            visitedPositions.Add(guardPosTuple);
            if (visitedPositions.Count(p => p.Item1 == guardPos.X && p.Item2 == guardPos.Y) > 3)
            {
                return [];
            }
        }

        return visitedPositions.Distinct().ToHashSet();
    }

    private int[] Rotate(int[] direction)
    {
        if (direction == Direction.Up)
            return Direction.Right;
        if (direction == Direction.Right)
            return Direction.Down;
        if (direction == Direction.Down)
            return Direction.Left;
        return Direction.Up;
    }

    private static class Direction
    {
        public static readonly int[] Up = [0, -1];
        public static readonly int[] Down = [0, 1];
        public static readonly int[] Right = [1, 0];
        public static readonly int[] Left = [-1, 0];
    }
}