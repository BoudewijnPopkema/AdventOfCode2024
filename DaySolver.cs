namespace AdventOfCode2024;

public interface IDaySolver
{
    public string Solve(string[] input);
    public int Day { get; }
    public int Part { get; }
}