namespace SearchText;

public interface ITextOfFile
{
    static void SetWordStore(TextOfFile textOfFile)
    {
        HashSet<string> words = textOfFile.Text!.Split(' ').ToHashSet();
        words.Remove("");
        textOfFile.WordStore = words;
    }
}