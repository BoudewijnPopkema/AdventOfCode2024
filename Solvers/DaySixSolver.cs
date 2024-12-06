namespace AdventOfCode2024.Solvers;
public class DaySixSolver : DaySolver
{
    public override int Day => 6;
    private char[][]? _grid;
    private int _gridSize => _grid![0].Length;
    private bool WithinGrid(dynamic pos) => pos.X >= 0 && pos.X < _gridSize && pos.Y >= 0 && pos.Y < _gridSize; 
    public override void LoadInputData(string[] input)
    {
        _grid = input.Select(s => s.ToCharArray()).ToArray();
    }
    public override string SolvePartOne()
    {
        var guardY = _grid!.ToList().FindIndex(s => s.Contains('^'));
        var guardX = _grid![guardY].ToList().FindIndex(s => s == '^');
        var guardPos = new {X = guardX, Y = guardY};
        var direction = Direction.Up;
        HashSet<(int,int)> visitedPositions = [(guardX, guardY)];

        while (WithinGrid(guardPos))
        {
            var nextPosition = new {X = guardPos.X + direction[0], Y = guardPos.Y + direction[1]};

            if (!WithinGrid(nextPosition))
            {
                break;
            }
            if (_grid[nextPosition.Y][nextPosition.X] == '#')
            {
                direction = Rotate(direction);
                continue;
            }

            guardPos = nextPosition;
            visitedPositions.Add(new ValueTuple<int, int>(guardPos.X, guardPos.Y));
        }

        return visitedPositions.Count.ToString();
    }
    public override string SolvePartTwo()
    {
        throw new NotImplementedException();
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