namespace SearchText;

public interface IInputOutput
{
    public string? GetInput();
    public void PrintList(List<string> candidatePaths);
}