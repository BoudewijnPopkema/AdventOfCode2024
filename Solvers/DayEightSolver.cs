namespace AdventOfCode2024.Solvers;

public class DayEightSolver : DaySolver
{
    public override int Day => 8;
    private char?[][] _grid;
    private int _gridSize;

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
    }

    public override string SolvePartOne()
    {
        var pairs = GetPairs();
        var antiNodes = GetAntiNodeLocations(pairs);

        return antiNodes.Count.ToString();
    }

    public override string SolvePartTwo()
    {
        throw new NotImplementedException();
    }
    
    private List<(int X,int Y)> GetAntiNodeLocations(List<(Antenna, Antenna)> pairs)
    {
        HashSet<(int X, int Y)> hashSet = [];
        pairs.SelectMany(GetAntiNodesForPair)
            .Where(an => an.X >= 0 && an.X < _gridSize && an.Y >= 0 && an.Y < _gridSize)
            .ToList()
            .ForEach(x => hashSet.Add(x));

        return hashSet.ToList();
    }

    private List<(int X,int Y)> GetAntiNodesForPair((Antenna, Antenna) pair)
    {
        var deltaX = pair.Item1.X - pair.Item2.X;
        var deltaY = pair.Item1.Y - pair.Item2.Y;

        var antiNodeOne = (pair.Item2.X + deltaX, pair.Item2.Y + deltaY);
        var antiNodeTwo = (pair.Item1.X - deltaX, pair.Item1.Y - deltaY);

        return [antiNodeOne, antiNodeTwo];
    }

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
                    result.Add(new Antenna{X = x, Y = y, Frequency = freq!.Value, Id = i});
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