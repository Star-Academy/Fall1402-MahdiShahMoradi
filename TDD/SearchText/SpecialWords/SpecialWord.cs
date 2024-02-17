namespace SearchText.SpecialWords;

public abstract class SpecialWord
{
    public char Sign;

    public List<string> Words = new();

    public virtual void AddSpecificWord(string word)
    {
        Words.Add(word.Substring(1));
    }
}