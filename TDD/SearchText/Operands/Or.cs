namespace SearchText.Operands;

public class Or : Operand
{
    public Or()
    {
        Sign = '+';
    }

    public override List<int> GetIndexesContainsWord(WordModel wordModel, List<string> words)
    {
        var output = wordModel.PathKeeper.GetAllIndexes();
        if (words.Count == 0) return output;
        output = [];
        foreach (var str in words)
        {
            if (wordModel.WordMapper!.ContainsKey(str))
                output = output.Union(wordModel.WordMapper[str]).ToList();
        }

        return output;
    }
}