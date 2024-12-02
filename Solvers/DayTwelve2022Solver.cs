namespace AdventOfCode2024.Solvers;
public class DayTwelve2022Solver : DaySolver
{
    public override int Day => 122022;
    private List<Position> _map = [];
    private List<Position> _path = [];

    public override void LoadInputData(string[] input)
    {
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                _map.Add(new Position()
                {
                    X = x,
                    Y = y, 
                    Height = GetHeight(input[y][x])
                });
            }
        }
    }
    public override string SolvePartOne()
    {
        throw new NotImplementedException();
    }

    public override string SolvePartTwo()
    {
        throw new NotImplementedException();
    }

    private class Position
    {
        public int X { get; init; }
        public int Y { get; init; }
        public int Height { get; set; }

        public static bool operator ==(Position a, Position b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Position a, Position b)
        {
            return !(a == b);
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
}