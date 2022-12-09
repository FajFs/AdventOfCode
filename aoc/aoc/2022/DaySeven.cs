using System.IO;

public class DaySeven
{
    private readonly DataFetcher _dataFetcher;
    public DaySeven(DataFetcher dataFetcher)
    {
        _dataFetcher = dataFetcher ?? throw new ArgumentNullException(nameof(dataFetcher));
    }

    public async Task Run()
    {
        await _dataFetcher.GetAndStoreData();
        Part1();
        Part2();
    }

    private SortedDictionary<string, int> FileSystem { get; set; } = new SortedDictionary<string, int>();
    private string FileSystemPointer { get; set; } = "";
    private List<(string, string)> InstructionList { get; set; } = new();
    private Queue<int> FileSizeInDirectory { get; set; } = new();

    //could be a prettier setup but it is what it is...
    private void GenerateInstructionsAndOutput(IList<string> input)
    {
        //group all instructions
        InstructionList = input.Where(x => x.Contains('$'))
            .Select(x =>
            {
                var tmp = x.Split(' ');
                return (tmp[1], tmp.Length == 2 ? "" : tmp[2]);
            }).ToList();

        //sum of file sizes per ls command in sequential order
        FileSizeInDirectory = new Queue<int>();
        for (int i = 0; i < input.Count; i++)
        {
            if (input[i].Contains("ls"))
            {
                var output = 0;
                while (++i < input.Count && !input[i].Contains('$'))
                {
                    if (input[i].Contains("dir")) //ignore dir commands
                        continue;
                    output += int.Parse(input[i].Split(" ")[0]);
                }
                FileSizeInDirectory.Enqueue(output);
            }
        }
    }

    private void MapCurrentDirectory(string instruction, string directory)
    {
        if (MoveFileSystemPointer(instruction, directory)) 
            return;
        //when an ls command is encountered, add the sum of file sizes to the current directory
        FileSystem[FileSystemPointer] =  FileSizeInDirectory.Dequeue();
    }

    private bool MoveFileSystemPointer(string instruction, string directory)
    {
        if (instruction == "cd" && directory == "/")
            FileSystemPointer = "";
        else if (instruction == "cd" && directory == "..")
            FileSystemPointer = FileSystemPointer.Substring(0, FileSystemPointer.LastIndexOf('/'));
        else if (instruction == "cd")
            FileSystemPointer += "/" + directory;
        else return false;
        return true;
    }

    private void Part1()
    {
        var input = _dataFetcher.Parse<string>("\n");
        GenerateInstructionsAndOutput(input);
        InstructionList.ForEach(x => MapCurrentDirectory(x.Item1, x.Item2));

        var result = 0;
        foreach (var (path, value) in FileSystem)
        {
            var currentDirectorySize = FileSystem.Keys.Where(x => x.StartsWith(path)).Sum(x => FileSystem[x]);
            if (currentDirectorySize <= 100000)
                result += currentDirectorySize;
        }
        Console.WriteLine($"part 1: {result}");
    }
    
    private void Part2()
    {
        var input = _dataFetcher.Parse<string>("\n");
        GenerateInstructionsAndOutput(input);
        InstructionList.ForEach(x => MapCurrentDirectory(x.Item1, x.Item2));

        var result = int.MaxValue;
        var requiredFreeDiskSpace = 30000000;
        var freeDiskSpace = 70000000 - FileSystem.Keys.Where(x => x.StartsWith("")).Sum(x => FileSystem[x]);

        foreach (var (path, value) in FileSystem.Skip(1))
        {
            var currentDirectorySize = FileSystem.Keys.Where(x => x.StartsWith(path)).Sum(x => FileSystem[x]);
            if (freeDiskSpace + currentDirectorySize >= requiredFreeDiskSpace && currentDirectorySize < result)
                result = currentDirectorySize;
        }
        Console.WriteLine($"part 2: {result}");
    }
}