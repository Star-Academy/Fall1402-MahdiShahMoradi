namespace SearchText;

public class InterpretString
{
    public List<string> MainWords;
    public List<string> PlusWords;
    public List<string> NegativeWords;
    public string Line;
    public string[] Words;

    private InterpretString(string line)
    {
        Line = line;
        MainWords = new List<string>();
        PlusWords = new List<string>();
        NegativeWords = new List<string>();
    }

    public static InterpretString Create(string? line)
    {
        InterpretString interpretString = new InterpretString(line!);
        IInterpretString.SetSplitWords(interpretString);
        IInterpretString.SetSpecialWords(interpretString);
        return interpretString;
    }
    
    
}

public interface IInterpretString
{
    static void SetSpecialWords(InterpretString interpretString)
    {
        foreach (string str in interpretString.Words)
        {
            string uppercaseString = str.ToUpper();
            if (str[0] == '+')
            {
                AddMarkedWordToWords(interpretString.PlusWords, uppercaseString);
            }
            else if (str[0] == '-')
            {
                AddMarkedWordToWords(interpretString.NegativeWords, uppercaseString);
            }
            else
            {
                interpretString.MainWords.Add(uppercaseString);
            }
        }
    }

    private static void AddMarkedWordToWords(List<string> list, string word)
    {
        list.Add(word.Substring(1).ToUpper());
    }

    static void SetSplitWords(InterpretString interpretString)
    {
        HashSet<string> words = interpretString.Line.Split(' ').ToHashSet();
        words.Remove("");
        interpretString.Words = words.ToArray();
    }
}