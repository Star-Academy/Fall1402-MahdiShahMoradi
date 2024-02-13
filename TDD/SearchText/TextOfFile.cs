namespace SearchText;

public class TextOfFile(string path)
{
    public string? Text { get; private set; }
    public HashSet<string>? WordStore;

    private void FileToText()
    {
        Text = File.ReadAllText(path).ToUpper();
    }

    public static TextOfFile Create(string path)
    {
        TextOfFile textOfFile = new TextOfFile(path);
        textOfFile.FileToText();
        ITextOfFile.SetWordStore(textOfFile);
        return textOfFile;
    }
}