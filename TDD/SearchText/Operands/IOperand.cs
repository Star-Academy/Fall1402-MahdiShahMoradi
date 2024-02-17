namespace SearchText.Operands;

public abstract class Operand
{
    public char Sign;
    public abstract List<int> GetIndexesContainsWord(WordModel wordModel, List<string> words);
}