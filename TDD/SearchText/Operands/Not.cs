namespace SearchText.Operands;

public class Not : Operand
{
    public Not()
    {
        Sign = '-';
    }

    public override List<int> GetIndexesContainsWord(WordModel wordModel, List<string> words)
    {
        var output = wordModel.PathKeeper.GetAllIndexes();
        foreach (var str in words)
        {
            if (!wordModel.WordMapper!.ContainsKey(str)) continue;
            var commonItems = output.Intersect(wordModel.WordMapper[str]).ToList();
            foreach (var item in commonItems)
            {
                output.RemoveAll(x => x == item);
            }
        }

        return output;
    }
}