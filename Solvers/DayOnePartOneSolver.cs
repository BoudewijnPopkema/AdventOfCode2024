namespace AdventOfCode2024.Solvers;

public class DayOnePartOneSolver : IDaySolver
{
    public string Solve(string[] input)
    {
        var listOne = input.Select(x => int.Parse(x.Split("   ")[0])).Order().ToList();
        var listTwo = input.Select(x => int.Parse(x.Split("   ")[1])).Order().ToList();

        var totalDistance = listOne.Select(
            (t, i) => Math.Abs(t - listTwo[i])
        ).Sum();

        return totalDistance.ToString();
    }

    public int Day => 1;
    public int Part => 1;
}