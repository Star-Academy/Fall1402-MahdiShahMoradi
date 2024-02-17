using SearchText.SpecialWords;

namespace SearchText;

public class InterpretString
{
    private readonly string _line;
    private string[]? _words;
    public SpecialWord[] Specials;
    

    private InterpretString(string line, SpecialWord[] specials)
    {
        _line = line;
        Specials = specials;
    }

    public static InterpretString Create(string? line, SpecialWord[] specials)
    {
        InterpretString interpretString = new InterpretString(line!, specials);
        interpretString.SetSplitWords();
        interpretString.SetSpecialWords();
        return interpretString;
    }
    
    private void SetSpecialWords()
    {
        foreach (var str in _words!)
        {
            var uppercaseString = str.ToUpper();
            var isAddToAnyGroup = false;
            foreach (var specialWord in Specials)
            {
                if (uppercaseString[0] != specialWord.Sign) continue;

                isAddToAnyGroup = true;
                specialWord.AddSpecificWord(uppercaseString);
                break;
            }
            if (!isAddToAnyGroup)
            {
                Specials[0].AddSpecificWord(uppercaseString);
            }
        }
    }

    private void SetSplitWords()
    {
        var words = this._line.Split(' ').ToHashSet();
        words.Remove("");
        _words = words.ToArray();
    }
}