namespace SearchText.Operands;

public class And : Operand
{
    public And()
    {
        Sign = ' ';
    }

    public override List<int> GetIndexesContainsWord(WordModel wordModel, List<string> words)
    {
        List<int> output = wordModel.PathKeeper.GetAllIndexes();
        if (words.Count == 0)
        {
            output.RemoveRange(0, output.Count);
        }
        else
        {
            for (int i = 0; i < words.Count; i++)
            {
                var searchString = words[i].ToUpper();
                if (wordModel.WordMapper!.ContainsKey(searchString))
                {
                    output = output.Intersect(wordModel.WordMapper[searchString]).ToList();
                }
                else
                {
                    output.RemoveRange(0, output.Count);
                }
            }
        }

        return output;
    }
}