namespace AdventOfCode2024;

public abstract class DaySolver
{
    public abstract void LoadInputData(string[] input);
    public abstract string SolvePartOne();
    public abstract string SolvePartTwo();
    public abstract int Day { get; }
}