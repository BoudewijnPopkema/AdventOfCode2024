using System.IO.Compression;
using System.Numerics;

namespace AdventOfCode2024.Solvers;

public class Day11Solver : DaySolver
{
    public override int Day => 11;
    private List<BigInteger> _stones = [];

    public override void LoadInputData(string[] input)
    {
        _stones = input[0].Split(" ").Select(BigInteger.Parse).ToList();
    }

    public override string SolvePartOne()
    {
        return ProcessBlinks(25);
    }

    public override string SolvePartTwo()
    {
        return ProcessBlinks(75);
    }

    private string ProcessBlinks(int blinks)
    {
        var stonesCopy = _stones.ToList();
        var numberCounts = stonesCopy.Select(x => new NumberCount(x, 1)).ToList();

        for (var i = 0; i < blinks; i++)
        {
            var newCounts = (
                from stone in numberCounts
                let splitStoneResults = StoneAfterBlink(stone.Number)
                from number in splitStoneResults
                select new NumberCount(number, stone.Count)
            ).ToList();

            numberCounts = newCounts.GroupBy(
                c => c.Number,
                c => c.Count,
                (number, count) => new NumberCount(number, count.Sum())
            ).ToList();
        }

        return numberCounts.Sum(nc => nc.Count).ToString();
    }

    private static List<BigInteger> StoneAfterBlink(BigInteger stone)
    {
        if (stone == 0)
            return [1];

        var stoneLengthIsEven = stone.ToString().Length % 2 == 0;
        if (stoneLengthIsEven)
        {
            return SplitStone(stone);
        }

        return [stone * 2024];
    }

    private static List<BigInteger> SplitStone(BigInteger stone)
    {
        var halfLength = stone.ToString().Length / 2;
        var firstHalf = stone.ToString()[..halfLength];
        var secondHalf = stone.ToString()[halfLength..];
        return [BigInteger.Parse(firstHalf), BigInteger.Parse(secondHalf)];
    }

    private readonly struct NumberCount(BigInteger number, long count)
    {
        public BigInteger Number { get; } = number;
        public long Count { get; } = count;
    }
}