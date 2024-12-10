namespace AdventOfCode2024.Solvers;

public class Day10Solver : DaySolver
{
    public override int Day => 10;
    private static int[][]? _grid;
    private static int _gridSize;

    public override void LoadInputData(string[] input)
    {
        _grid = input
            .Select(s => s.Select(c => int.Parse(c.ToString())).ToArray())
            .ToArray();
        _gridSize = _grid.Length;
    }

    public override string SolvePartOne()
    {
        var trailHeads = GetTrailheads();
        return trailHeads.Sum(t => t.GetScore(false)).ToString();
    }

    public override string SolvePartTwo()
    {
        var trailHeads = GetTrailheads();
        return trailHeads.Sum(t => t.GetScore(true)).ToString();
    }
    
    private static List<Trailhead> GetTrailheads()
    {
        var result = new List<Trailhead>();
        for (var y = 0; y < _gridSize; y++)
        {
            for (var x = 0; x < _gridSize; x++)
            {
                var height = _grid![y][x];
                if (height == 0)
                    result.Add(new Trailhead(x, y));
            }
        }

        return result;
    }

    private class Trailhead(int startX, int startY)
    {
        public int GetScore(bool useDistinctMethod)
        {
            Queue<Position> trailWalkers = new();
            trailWalkers.Enqueue(new Position(startX, startY));

            HashSet<Position> peaksReached = [];
            var distinctScore = 0;
            while (trailWalkers.Count > 0)
            {
                var trailWalker = trailWalkers.Dequeue();
                var height = _grid![trailWalker.Y][trailWalker.X];
                if (height == 9)
                {
                    distinctScore++;
                    peaksReached.Add(trailWalker);
                    continue;
                }

                var nextPositions = GetNextPositions(trailWalker);
                nextPositions.ForEach(np => trailWalkers.Enqueue(np));
            }

            return useDistinctMethod ? distinctScore : peaksReached.Count;
        }
    }

    private static List<Position> GetNextPositions(Position position)
    {
        List<(int deltaX, int deltaY)> directions = [(0, 1), (0, -1), (-1, 0), (1, 0)];
        List<Position> result = [];

        foreach (var direction in directions)
        {
            var newPosition = new Position(position.X + direction.deltaX, position.Y + direction.deltaY);
            if (!WithinGrid(newPosition))
                continue;

            var currentHeight = _grid![position.Y][position.X];
            var newHeight = _grid[newPosition.Y][newPosition.X];
            if (newHeight != currentHeight + 1)
                continue;

            result.Add(newPosition);
        }

        return result;
    }

    private static bool WithinGrid(Position position) => position.X >= 0 && position.Y >= 0 && position.X < _gridSize && position.Y < _gridSize;

    private class Position(int x, int y)
    {
        public int X { get; } = x;
        public int Y { get; } = y;

        public override bool Equals(object? obj)
        {
            if (obj is not Position position)
                return false;

            return position.X == X && position.Y == Y;
        }

        public override int GetHashCode()
        {
            return int.Parse(X + "000" + Y);
        }
    }
}