using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandervanteinde.AdventOfCode2021.Solutions.Utils;

internal class FileReader
{
    public string Input { get; }

    public FileReader(string input)
    {
        Input = input;
    }

    public IEnumerable<string> ReadLineByLine()
    {
        return Input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
    }

    public IEnumerable<char> ReadCharByChar()
    {
        return Input;
    }
}
