namespace SearchText.SpecialWords;

public class Main : SpecialWord
{
    public Main()
    {
        Sign = ' ';
    }

    public override void AddSpecificWord(string str)
    {
        Words.Add(str);
    }
}