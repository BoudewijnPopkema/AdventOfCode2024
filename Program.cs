using System.Diagnostics;
using AdventOfCode2024;
using AdventOfCode2024.Solvers;

var solvers = GetSolvers();

Console.Write("Day: ");
var daySolver = GetSolver(Console.ReadLine()!); 
Console.Write("Input file path: ");
var input = GetInputFromFile(Console.ReadLine()!);

Console.WriteLine("Running solver for day " + daySolver.Day + "...");
daySolver.LoadInputData(input);
try
{
    var stopwatch = new Stopwatch();
    stopwatch.Start();
    Console.Write("Part one: " + daySolver.SolvePartOne());
    Console.WriteLine($" in {stopwatch.ElapsedMilliseconds}ms");
    stopwatch.Restart();
    Console.Write("Part two: " + daySolver.SolvePartTwo());
    Console.WriteLine($" in {stopwatch.ElapsedMilliseconds}ms");
}
catch (NotImplementedException)
{
    Console.WriteLine("Part two not implemented yet.");
}

return;

// Local functions
DaySolver GetSolver(string inputDay)
{
    var day = int.Parse(inputDay);
    
    var solver = solvers.FirstOrDefault(s => s.Day == day);
    if (solver == null)
    {
        throw new ArgumentException("No solver found for day " + day);
    }
    
    return solver;
}

string[] GetInputFromFile(string inputFilePath)
{ 
    inputFilePath = inputFilePath.Replace("\"", "");
    var lines = File.ReadAllLines(inputFilePath);
    if (string.IsNullOrWhiteSpace(lines.Last()))
        lines = lines.SkipLast(1).ToArray();
    return lines;
}

List<DaySolver> GetSolvers() => [
    new Day1Solver(), 
    new Day2Solver(),
    new Day3Solver(),
    new Day4Solver(),
    new Day5Solver(),
    new Day6Solver(),
    new Day7Solver(),
    new Day8Solver(),
    new Day9Solver(),
    new Day10Solver(),
    new Day11Solver(),
    new Day12Solver(),
    new Day12_2022Solver()
];
