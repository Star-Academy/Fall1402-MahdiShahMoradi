namespace SearchText;

public class TerminalManager : IInputOutput
{
    public string? GetInput()
    {
        return Console.ReadLine();
    }

    public void Print(string str)
    {
        Console.WriteLine(str);
    }
    
    public void PrintList(List<string> candidatePaths)
    {
        foreach (var path in candidatePaths)
        {
            Console.WriteLine(path);
        }
    }
}