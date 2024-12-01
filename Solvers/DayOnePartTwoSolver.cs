namespace AdventOfCode2024.Solvers;

public class DayOnePartTwoSolver : IDaySolver
{
    public int Day => 1; 
    public int Part => 2;

    public string Solve(string[] input)
    {
        var listOne = input.Select(x => int.Parse(x.Split("   ")[0])).Order().ToList();
        var listTwo = input.Select(x => int.Parse(x.Split("   ")[1])).Order().ToList();

        var totalScore = listOne.Sum(
            number => number * listTwo.Count(x => x == number)
        );

        return totalScore.ToString();
    }
}