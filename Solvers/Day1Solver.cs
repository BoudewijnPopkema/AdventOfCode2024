namespace AdventOfCode2024.Solvers;

public class Day1Solver : DaySolver
{
    public override int Day => 1;
    
    private List<int> _listOne = [];
    private List<int> _listTwo = [];

    public override void LoadInputData(string[] input)
    {
        _listOne = input.Select(x => int.Parse(x.Split("   ")[0])).Order().ToList();
        _listTwo = input.Select(x => int.Parse(x.Split("   ")[1])).Order().ToList();
    }
    public override string SolvePartOne()
    {
        var totalDistance = _listOne.Select(
            (t, i) => Math.Abs(t - _listTwo[i])
        ).Sum();

        return totalDistance.ToString();
    }
    
    public override string SolvePartTwo()
    {
        var totalScore = _listOne.Sum(
            number => number * _listTwo.Count(x => x == number)
        );

        return totalScore.ToString();
    }
}