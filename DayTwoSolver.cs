namespace AdventOfCode2024;
public class DayTwoSolver : DaySolver
{
    private List<List<int>> _reports = [];
    public override void LoadInputData(string[] input)
    {
        _reports = input.Select(x => x.Split(' ').Select(int.Parse).ToList()).ToList();
    }

    public override string SolvePartOne()
    {
        var safeReports = 0;

        foreach (var report in _reports)
        {
            var allIncreasing = report.SequenceEqual(report.Order());
            var allDecreasing = report.SequenceEqual(report.OrderDescending());

            var smallDifferences = true;
            var prevNr = report[0];

            foreach (var currentNr in report.Skip(1))
            {
                var difference = Math.Abs(prevNr - currentNr);
                prevNr = currentNr;
                if (difference is >= 1 and <= 3) continue;
                smallDifferences = false;
            }

            if ((allIncreasing || allDecreasing) && smallDifferences)
            {
                safeReports++;
            }
        }

        return safeReports.ToString();
    }

    public override string SolvePartTwo()
    {
        var safeReports = 0;

        foreach (var report in _reports)
        {
            List<List<int>> reportVariants = [report];

            for (var i = 0; i < report.Count; i++)
            {
                var variant = report.ToList();
                variant.RemoveAt(i);
                reportVariants.Add(variant);
            }
            
            if (reportVariants.Any(CheckIfSafe))
            {
                safeReports++;
            }
        }

        return safeReports.ToString();
    }

    private static bool CheckIfSafe(List<int> variant)
    {
        var allIncreasing = variant.SequenceEqual(variant.Order());
        var allDecreasing = variant.SequenceEqual(variant.OrderDescending());

        if (!allIncreasing && !allDecreasing)
        {
            return false;
        }
        
        var smallDifferences = true;
        var prevNr = variant[0];

        foreach (var currentNr in variant.Skip(1))
        {
            var difference = Math.Abs(prevNr - currentNr);
            prevNr = currentNr;
            if (difference is >= 1 and <= 3) continue;
            smallDifferences = false;
        }

        return smallDifferences;
    }

    public override int Day => 2;
}