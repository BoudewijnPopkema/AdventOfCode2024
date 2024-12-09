namespace AdventOfCode2024.Solvers;

public class DayNineSolver : DaySolver
{
    public override int Day => 9;
    private List<ushort?> _backupDisk = [];
    public override void LoadInputData(string[] input)
    {
        var block = true;
        ushort id = 0;
        foreach (var blockSize in input[0].Select(c => ushort.Parse(c.ToString())))
        {
            for (var i = 0; i < blockSize; i++)
            {
                if (block)
                    _backupDisk.Add((ushort)(id / 2));
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
        CompactDisk(diskMap);
        return ComputeChecksum(diskMap).ToString();
    }

    public override string SolvePartTwo()
    {
        var diskMap = _backupDisk.ToList();
        CompactDiskUnfragmented(diskMap);
        return ComputeChecksum(diskMap).ToString();
    }

    private static void CompactDisk(List<ushort?> diskMap)
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
    
    private static void CompactDiskUnfragmented(List<ushort?> diskMap)
    {
        List<ushort> movedBlockIds = [];
        var blockIndex = (ushort)(diskMap.Count - 1);
        while (true)
        {
            var blockToMove = GetNextBlock(blockIndex);
            blockIndex = blockToMove.Index;
            if (blockIndex == 0)
                return;
            
            var fittingSpaceIndex = GetFittingSpaceIndex(blockToMove.Size);
            if (fittingSpaceIndex != null)
            {
                Move(blockToMove, fittingSpaceIndex.Value);
                movedBlockIds.Add(blockToMove.Id);
            }

            
        }

        // Local methods
        Block GetNextBlock(ushort index)
        {
            while (true)
            {
                var nextBlockEndIndex = diskMap.FindLastIndex(index, x => x.HasValue && !movedBlockIds.Contains(x.Value));
                var id = diskMap[nextBlockEndIndex];
                var nextBlockStartIndex = diskMap.FindLastIndex(nextBlockEndIndex, x => !x.HasValue || x.Value != id) + 1;
                var size = nextBlockEndIndex + 1 - nextBlockStartIndex;

                return new Block(id!.Value, (ushort)nextBlockStartIndex, (byte)size);
            }
        }

        int? GetFittingSpaceIndex(int size)
        {
            var nextEmptyIndex = 0;
            while (true)
            {
                nextEmptyIndex = diskMap.FindIndex(nextEmptyIndex + 1, p => !p.HasValue);
                if (nextEmptyIndex == -1)
                    return null;
                
                var spaceSize = diskMap.FindIndex(nextEmptyIndex, p => p.HasValue) - nextEmptyIndex;
                if (spaceSize >= size)
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

    private static long ComputeChecksum(List<ushort?> diskMap)
    {
        long checksum = 0;
        for (var i = 0; i < diskMap.Count ; i++)
        {
            var id = diskMap[i];
            if (id == null)
                continue;

            checksum += i * id.Value;
        }
        
        return checksum;
    }

    private readonly struct Block(ushort id, ushort index, byte size)
    {
        public ushort Id { get; } = id;
        public ushort Index { get; } = index;
        public byte Size { get;} = size;
    }
}