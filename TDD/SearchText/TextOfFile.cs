namespace SearchText;

public class TextOfFile(string path)
{

    private string? _text { get; set; }


    public string? Text => _text;

    private void FileToText()
    {
        _text = File.ReadAllText(path);
    }

    public static TextOfFile Create(string path)
    {
        TextOfFile textOfFile = new TextOfFile(path);
        textOfFile.FileToText();
        return textOfFile;
    }
    
    
}