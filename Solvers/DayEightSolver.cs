namespace AdventOfCode2024.Solvers;

public class DayEightSolver : DaySolver
{
    public override int Day => 8;
    private char?[][] _grid;
    private int _gridSize;
    private List<(Antenna, Antenna)> _pairs = [];

    public override void LoadInputData(string[] input)
    {
        _gridSize = input.Length;
        _grid = new char?[_gridSize][];
        for (var y = 0; y < _gridSize; y++)
        {
            _grid[y] = new char?[_gridSize];
            for (var x = 0; x < _gridSize; x++)
            {
                var value = input[y][x];
                if (value == '.')
                    continue;

                _grid[y][x] = value;
            }
        }

        _pairs = GetPairs();
    }

    public override string SolvePartOne()
    {
        return GetAntiNodeLocations(_pairs, false).Count.ToString();
    }

    public override string SolvePartTwo()
    {
        return GetAntiNodeLocations(_pairs, true).Count.ToString();
    }

    private List<(int X, int Y)> GetAntiNodeLocations(List<(Antenna, Antenna)> pairs, bool line)
    {
        var antiNodes = pairs.SelectMany(p => GetAntiNodesForPair(p, line)).ToList();

        if (line)
            antiNodes.AddRange(pairs.SelectMany(p => new List<(int X, int Y)> { (p.Item1.X, p.Item1.Y), (p.Item2.X, p.Item2.Y) }).ToList());
        else 
            antiNodes.RemoveAll(an => !WithinGrid(an.X, an.Y));

        return antiNodes.Distinct().ToList();
    }

    private List<(int X, int Y)> GetAntiNodesForPair((Antenna, Antenna) pair, bool line)
        => line ? GetLineAntiNodesForPair(pair) : GetAntiNodesForPair(pair);

    private List<(int X, int Y)> GetAntiNodesForPair((Antenna, Antenna) pair)
    {
        var deltaX = pair.Item1.X - pair.Item2.X;
        var deltaY = pair.Item1.Y - pair.Item2.Y;

        var antiNodeOne = (pair.Item1.X + deltaX, pair.Item1.Y + deltaY);
        var antiNodeTwo = (pair.Item2.X - deltaX, pair.Item2.Y - deltaY);

        return [antiNodeOne, antiNodeTwo];
    }

    private List<(int X, int Y)> GetLineAntiNodesForPair((Antenna, Antenna) pair)
    {
        List<(int X, int Y)> result = [];

        var deltaX = pair.Item1.X - pair.Item2.X;
        var deltaY = pair.Item1.Y - pair.Item2.Y;

        var x = pair.Item1.X + deltaX;
        var y = pair.Item1.Y + deltaY;
        while (WithinGrid(x, y))
        {
            result.Add((x, y));
            x += deltaX;
            y += deltaY;
        }

        x = pair.Item2.X - deltaX;
        y = pair.Item2.Y - deltaY;
        while (WithinGrid(x, y))
        {
            result.Add((x, y));
            x -= deltaX;
            y -= deltaY;
        }

        return result;
    }

    private bool WithinGrid(int x, int y) => x >= 0 && x < _gridSize && y >= 0 && y < _gridSize;

    private List<(Antenna, Antenna)> GetPairs()
    {
        List<(Antenna, Antenna)> pairs = [];
        var antennas = GetAntennas();
        var peers = antennas.ToList();

        foreach (var antenna in antennas)
        {
            var selfIndex = peers.FindIndex(p => p.Id == antenna.Id);
            peers.RemoveAt(selfIndex);
            var matches = peers.Where(a => a.Frequency == antenna.Frequency).ToList();
            matches.ForEach(match => pairs.Add(new ValueTuple<Antenna, Antenna>(antenna, match)));
        }

        return pairs.ToList();
    }

    private List<Antenna> GetAntennas()
    {
        List<Antenna> result = [];
        var i = 0;
        for (var y = 0; y < _gridSize; y++)
        {
            for (var x = 0; x < _gridSize; x++)
            {
                var freq = _grid[y][x];
                if (freq.HasValue)
                {
                    result.Add(new Antenna { X = x, Y = y, Frequency = freq!.Value, Id = i });
                    i++;
                }
            }
        }

        return result;
    }

    private struct Antenna
    {
        public int X { get; init; }
        public int Y { get; init; }
        public char Frequency { get; init; }
        public int Id { get; init; }
    }
}