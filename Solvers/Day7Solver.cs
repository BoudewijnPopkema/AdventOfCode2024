namespace AdventOfCode2024.Solvers;

public class Day7Solver : DaySolver
{
    public override int Day => 7;
    private List<List<long>>? _equations;

    public override void LoadInputData(string[] input)
    {
        _equations = input.Select(s => s
            .Replace(":", "")
            .Split(" ")
            .Select(long.Parse)
            .ToList()
        ).ToList();
    }

    public override string SolvePartOne()
    {
        return Solve(useThirdOp: false);
    }
    
    public override string SolvePartTwo()
    {
        return Solve(useThirdOp: true);
    }

    private string Solve(bool useThirdOp)
    {
        long total = 0;
        foreach (var equation in _equations!)
        {
            var equationCopy = equation.ToList();
            var expectedResult = equationCopy.First();
            equationCopy.RemoveAt(0);

            var configs = GetOperatorConfigs(equationCopy.Count - 1, useThirdOp);
            var valid = configs.Any(c =>
            {
                var valid = Compute(equationCopy, c) == expectedResult;
                return valid;
            });
            
            if (valid)
                total += expectedResult;
        }

        return total.ToString();
    }

    private static long Compute(List<long> equation, List<char> config)
        => Compute(new Queue<long>(equation), new Queue<char>(config));

    private static long Compute(Queue<long> equation, Queue<char> config)
    {
        var total = equation.Dequeue();
        while (config.Count > 0)
        {
            // Add previous value to front if present, always reset cuz lazy
            var value = equation.Dequeue();
            var op = config.Dequeue();
            switch (op)
            {
                case '*':
                    total *= value;
                    break;
                case '+':
                    total += value;
                    break;
                case '|':
                    total = long.Parse(total.ToString() + value);
                    break;
            }
        }

        return total;
    }

    private static List<List<char>> GetOperatorConfigs(long operatorCount, bool useThirdOp)
    {
        List<List<char>> result = [[]];
        for (var i = 0; i < operatorCount; i++)
        {
            List<List<char>> newDuplicates = [];
            foreach (var list in result)
            {
                var duplicate = list.ToList();
                if (useThirdOp)
                    AddThirdOperator(list, newDuplicates);
                
                list.Add('+');
                duplicate.Add('*');

                newDuplicates.Add(duplicate);
            }

            result.AddRange(newDuplicates);
        }

        return result;

        
        void AddThirdOperator(List<char> list, List<List<char>> newDuplicates)
        {
            var duplicate2 = list.ToList();
            duplicate2.Add('|');
            newDuplicates.Add(duplicate2);
        }
    }
}