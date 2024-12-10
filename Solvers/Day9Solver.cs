namespace AdventOfCode2024.Solvers;

public class Day9Solver : DaySolver
{
    public override int Day => 9;
    private readonly List<int?> _backupDisk = [];

    public override void LoadInputData(string[] input)
    {
        var block = true;
        var id = 0;
        foreach (var blockSize in input[0].Select(c => int.Parse(c.ToString())))
        {
            for (var i = 0; i < blockSize; i++)
            {
                if (block)
                    _backupDisk.Add(id / 2);
                else
                    _backupDisk.Add(null);
            }

            block = !block;
            id++;
        }
    }

    public override string SolvePartOne()
    {
        var diskMap = _backupDisk.ToList();
        Console.WriteLine(string.Join("|", diskMap.Select(n => n == null ? "." : n.ToString())));
        CompactDisk(diskMap);
        return ComputeChecksum(diskMap).ToString();
    }

    public override string SolvePartTwo()
    {
        var diskMap = _backupDisk.ToList();
        CompactDiskUnfragmented(diskMap);
        return ComputeChecksum(diskMap).ToString();
    }

    private static void CompactDisk(List<int?> diskMap)
    {
        while (true)
        {
            var firstNullIndex = diskMap.FindIndex(n => !n.HasValue);
            var lastNumberIndex = diskMap.FindLastIndex(n => n.HasValue);

            if (lastNumberIndex < firstNullIndex)
                return;

            diskMap[firstNullIndex] = diskMap[lastNumberIndex];
            diskMap[lastNumberIndex] = null;
        }
    }

    private static void CompactDiskUnfragmented(List<int?> diskMap)
    {
        List<int> movedBlockIds = [];
        var blockIndex = diskMap.Count - 1;
        while (true)
        {
            var blockToMove = GetNextBlock(blockIndex);
            blockIndex = blockToMove.Index;
            if (blockIndex == 0)
                return;

            var fittingSpaceIndex = GetFittingSpaceIndex(blockToMove);
            if (fittingSpaceIndex != null)
            {
                Move(blockToMove, fittingSpaceIndex.Value);
                movedBlockIds.Add(blockToMove.Id);
            }
            else
            {
                blockIndex--;
            }
        }

        // Local methods
        Block GetNextBlock(int index)
        {
            while (true)
            {
                var nextBlockEndIndex = diskMap.FindLastIndex(index, x => x.HasValue && !movedBlockIds.Contains(x.Value));
                var id = diskMap[nextBlockEndIndex];
                var nextBlockStartIndex = diskMap.FindLastIndex(nextBlockEndIndex, x => !x.HasValue || x.Value != id) + 1;
                var size = nextBlockEndIndex + 1 - nextBlockStartIndex;

                return new Block(id!.Value, nextBlockStartIndex, (byte)size);
            }
        }

        int? GetFittingSpaceIndex(Block block)
        {
            var nextEmptyIndex = 0;
            while (true)
            {
                nextEmptyIndex = diskMap.FindIndex(nextEmptyIndex + 1, p => !p.HasValue);
                if (nextEmptyIndex == -1 || nextEmptyIndex > block.Index)
                    return null;

                var spaceSize = diskMap.FindIndex(nextEmptyIndex, p => p.HasValue) - nextEmptyIndex;
                if (spaceSize >= block.Size)
                {
                    return nextEmptyIndex;
                }
            }
        }

        void Move(Block block, int index)
        {
            for (var i = 0; i < block.Size; i++)
            {
                diskMap[index + i] = block.Id;
                diskMap[block.Index + i] = null;
            }
        }
    }

    private static long ComputeChecksum(List<int?> diskMap)
    {
        long checksum = 0;
        for (var i = 0; i < diskMap.Count; i++)
        {
            var id = diskMap[i];
            if (id == null)
                continue;

            checksum += i * id.Value;
        }

        return checksum;
    }

    private readonly struct Block(int id, int index, byte size)
    {
        public int Id { get; } = id;
        public int Index { get; } = index;
        public byte Size { get; } = size;
    }
}