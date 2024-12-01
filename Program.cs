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
    Console.WriteLine("Part one: " + daySolver.SolvePartOne());
    Console.WriteLine("Part two: " + daySolver.SolvePartTwo());
}
catch (NotImplementedException)
{
    Console.WriteLine("Part two not implemented yet.");
}

return;

// Local functions
List<DaySolver> GetSolvers() => [
    new DayOneSolver(), 
];

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
    return File.ReadAllLines(inputFilePath);
}