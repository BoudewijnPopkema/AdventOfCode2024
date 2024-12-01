using AdventOfCode2024;
using AdventOfCode2024.Solvers;

var solvers = GetSolvers();

Console.Write("Day and part(0 0): ");
var daySolver = GetSolver(Console.ReadLine()!); 
Console.Write("Input file path: ");
var input = GetInputFromFile(Console.ReadLine()!);

Console.WriteLine("Running solver for day " + daySolver.Day + "...");
var solution = daySolver.Solve(input);
Console.WriteLine("Solution: " + solution);
return;

List<IDaySolver> GetSolvers() => [
    new DayOnePartOneSolver(), 
    new DayOnePartTwoSolver()
];

IDaySolver GetSolver(string inputDayAndPart)
{
    var day = int.Parse(inputDayAndPart.Split(" ")[0]);
    var part = int.Parse(inputDayAndPart.Split(" ")[1]);
    
    var solver = solvers.FirstOrDefault(s => s.Day == day && s.Part == part);
    if (solver == null)
    {
        throw new ArgumentException("No solver found for day/part " + day + "/" + part);
    }
    
    return solver;
}

string[] GetInputFromFile(string inputFilePath)
{
    return File.ReadAllLines(inputFilePath.Replace("\"", ""));
}