using System.Diagnostics;
using System.Reflection.Metadata;

namespace Sandervanteinde.AdventOfCode.Solutions._2022;

internal class Day07 : BaseSolution
{
    public Day07()
        : base("No Space Left On Device", 2022, 7)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var root = ParseDirectories(reader);
        var sum = 0UL;
        foreach(var directory in root.EnumerateSelfAndChildren())
        {
            if(directory.TotalSize <= 100000)
            {
                sum += directory.TotalSize;
            }
        }
        return sum;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var root = ParseDirectories(reader);
        var totalSizeOfFileSystem = 70000000UL;
        var totalSpaceAvailable = root.TotalSize;
        var missingSpace = 30000000 - (totalSizeOfFileSystem - totalSpaceAvailable);
        return root.EnumerateSelfAndChildren()
            .Where(x => x.TotalSize >= missingSpace)
            .Min(x => x.TotalSize);
    }

    private Directory ParseDirectories(FileReader reader)
    {
        var root = new Directory("/");
        var currentDirectory = root;
        foreach (var line in reader.ReadAsSpanLineByLine())
        {
            switch (line)
            {
                case ['$', ' ', 'c', 'd', ' ', .. var path]:
                    SetCurrentDirectory(path);
                    break;
                case ['$', ' ', 'l', 's']:
                    break;
                case ['d', 'i', 'r', ' ', .. var directoryName]:
                    currentDirectory.AddDirectory(directoryName.ToString());
                    break;
                default: // file with size
                    var split = line.SplitAtFirstOccurenceOf(' ');
                    currentDirectory.AddFile(split.Right.ToString(), ulong.Parse(split.Left));
                    break;
            }
        }

        return root;


        void SetCurrentDirectory(ReadOnlySpan<char> path)
        {
            currentDirectory = path switch
            {
                ['/'] => root,
                ['.', '.'] => currentDirectory.Parent ?? throw new Exception("No parent directory"),
                _ => currentDirectory.GetDirectory(path.ToString())
            };
        }
    }

    private class Directory
    {
        public Directory? Parent { get; init; }
        private Dictionary<string, ulong> _files = new();
        public IReadOnlyDictionary<string, ulong> Files => _files;
        private Dictionary<string, Directory> _directories = new();
        public IReadOnlyDictionary<string, Directory> Directories => _directories;

        public ulong OwnSize { get; private set; }
        public ulong TotalSize { get; private set; }

        public string Path { get; }

        public Directory(string path)
        {
            Path = path;
        }

        public void AddDirectory(string directory)
        {
            _directories.Add(directory, new Directory($"{Path}{directory}/") { Parent = this });
        }

        public Directory GetDirectory(string directory)
        {
            if(!_directories.TryGetValue(directory, out var matchedDirectory))
            {
                throw new Exception("Directory did not exist");
            }

            return matchedDirectory;
        }

        public void AddFile(string fileName, ulong size)
        {
            _files.Add(fileName, size);
            OwnSize += size;
            TotalSize += size;

            var parent = Parent;
            while(parent is not null)
            {
                parent.TotalSize += size;
                parent = parent.Parent;
            }
        }

        public IEnumerable<Directory> EnumerateSelfAndChildren()
        {
            yield return this;
            foreach(var childs in _directories.Values.SelectMany(dir => dir.EnumerateSelfAndChildren()))
            {
                yield return childs;
            }
        }
    }
}
