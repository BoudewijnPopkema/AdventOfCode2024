namespace AdventOfCode2024.Solvers;

public class Day12Solver : DaySolver
{
    public override int Day => 12;
    private char[][]? _grid;
    private int?[][]? _regionGrid;
    private int _gridSize;
    private List<Region> _regions = [];
    private static readonly List<Position> Directions = [new(-1, 0), new(1, 0), new(0, -1), new(0, 1)];
    public override void LoadInputData(string[] input)
    {
        _gridSize = input.Length;
        _grid = new char[_gridSize][];
        _regionGrid = new int?[_gridSize][];

        FillGrid();
        
        for (var y = 0; y < _gridSize; y++)
        {
            for (var x = 0; x < _gridSize; x++)
            {
                var knownRegion = _regionGrid[y][x].HasValue;

                if (knownRegion)
                    continue;

                var region = MapNewRegion(x, y);
                _regions.Add(region);
            }
        }

        return;

        void FillGrid()
        {
            for (var y = 0; y < _gridSize; y++)
            {
                _grid[y] = new char[_gridSize];
                _regionGrid[y] = new int?[_gridSize];
                for (var x = 0; x < _gridSize; x++)
                {

                    _grid[y][x] = input[y][x];
                }
            }
        }
    }

    private Region MapNewRegion(int x, int y)
    {
        var region = new Region()
        {
            Id = _regions.Count,
            PlantType = _grid![y][x],
            Positions = [new Position(x,y)]
        };

        _regionGrid![y][x] = region.Id;
        
        Queue<Position> walkers = new();
        walkers.Enqueue(new Position(x, y));
        
        while (walkers.Count > 0)
        {
            var walker = walkers.Dequeue();

            var nextPositions = GetNextPositions(walker, region);
            region.Positions.AddRange(nextPositions);
            nextPositions.ForEach(np => walkers.Enqueue(np));
        }

        return region;
    }

    private List<Position> GetNextPositions(Position walker, Region region)
    {
        List<Position> discoveredPositions = [];
        foreach (var direction in Directions)
        {
            var newPosition = new Position(walker.X + direction.X, walker.Y + direction.Y);
            if (!WithinGrid(newPosition) || region.Positions.Any(p => p.X == newPosition.X && p.Y == newPosition.Y))
                continue;
        
            var newPlantType = _grid![newPosition.Y][newPosition.X];
            if (newPlantType != region.PlantType)
                continue;

            discoveredPositions.Add(newPosition);
            _regionGrid![newPosition.Y][newPosition.X] = region.Id;
        }

        return discoveredPositions;
    }

    private bool WithinGrid(Position position) => position is { X: >= 0, Y: >= 0 } && position.X < _gridSize && position.Y < _gridSize;

    public override string SolvePartOne()
    {
        return _regions.Sum(GetRegionFenceCost).ToString();
    }

    private static int GetRegionFenceCost(Region region)
    {
        var cost = 0;
        foreach (var pos in region.Positions)
        {
            foreach (var direction in Directions)
            {
                var nextPos = new Position(pos.X + direction.X, pos.Y + direction.Y);
                
                if (!region.Positions.Any(p => p.X == nextPos.X && p.Y == nextPos.Y))
                    cost += region.Positions.Count;
            }
        }

        return cost;
    }

    public override string SolvePartTwo()
    {
        return _regions.Sum(r => CountRegionSides(r) * r.Positions.Count).ToString();
    }

    private int CountRegionSides(Region region)
    {
        var pos = region.Positions.First();
        return 0;
    }

    private class Region
    {
        public int Id { get; init; }
        public char PlantType { get; init; }
        public List<Position> Positions { get; init; } = [];
    }

    private struct Position(int x, int y)
    {
        public int X { get; } = x;
        public int Y { get; } = y;
    }
}