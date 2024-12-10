namespace AdventOfCode2024.Solvers;

public class Day12_2022Solver : DaySolver
{
    public override int Day => 122022;
    private Position? _start;
    private Position? _end;
    private static List<Position> _map = [];

    public override void LoadInputData(string[] input)
    {
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Trim().Length; x++)
            {
                var newPosition = new Position()
                {
                    X = x,
                    Y = y,
                    Height = GetHeight(input[y][x])
                };
                _map.Add(newPosition);

                if (input[y][x] == 'S')
                {
                    _start = newPosition;
                }

                if (input[y][x] == 'E')
                {
                    _end = newPosition;
                }
            }
        }
    }
    public override string SolvePartOne()
    {
        return FindShortestPath(_start!).ToString();
    }

    public override string SolvePartTwo()
    {
        var lowestPositions = _map.Where(p => p.Height == 0).ToList();
        return lowestPositions.Min(FindShortestPath).ToString();
    }

    private int FindShortestPath(Position startingPos)
    {
        _map.ForEach(p => p.Scanned = false);
        List<Position> currentPositions = [startingPos];
        List<Position> nextPositions = [];
        var distance = 1;
        while (true)
        {
            foreach (var pos in currentPositions)
            {
                var neighbors =
                    _map.Where(p => p.Scanned == false)
                        .Where(p => !nextPositions.Contains(p))
                        .Where(p => p.ReachableFrom(pos))
                        .Where(pos.IsClose)
                        .ToList();

                nextPositions.AddRange(neighbors);
                pos.Scanned = true;

                if (neighbors.Any(p => p == _end))
                {
                    return distance;
                }
            }

            if (nextPositions.Count == 0)
            {
                return int.MaxValue;
            }

            distance++;
            currentPositions = nextPositions.ToList();
            nextPositions.Clear();
        }
    }

    private static int GetHeight(char c)
    {
        return c switch
        {
            'S' => 0,
            'E' => 25,
            _ => c - 97
        };
    }

    private class Position
    {
        public int X { get; init; }
        public int Y { get; init; }
        public int Height { get; set; }
        public bool Scanned { get; set; }

        public bool ReachableFrom(Position p)
        {
            return Height <= p.Height + 1;
        }

        public bool IsClose(Position p)
        {
            return (Math.Abs(p.X - X) == 1 && p.Y == Y) // Left or right
                   || (Math.Abs(p.Y - Y) == 1 && p.X == X); // Up or down
        }
    }
}