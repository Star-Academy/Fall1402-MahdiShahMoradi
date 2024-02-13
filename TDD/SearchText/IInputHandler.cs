namespace SearchText;

public interface IHandleInput
{
    static void Handle(WordModel wordModel)
    {
        string line = Console.ReadLine();
        InterpretString interpretString = InterpretString.Create(line);
        List<int> candidateIndexes = UnionLists(wordModel, interpretString);
    }

    static List<int> UnionLists(WordModel wordModel, InterpretString interpretString)
    {
        List<int> output = GetAllIndexes(wordModel);
        List<int> mainWordsList = GetMainWordsList(wordModel, interpretString);
        List<int> negativeWordsList = GetNegativeWordsList(wordModel, interpretString);
        // List<int> plusWordsList = GetPlusWordsList(wordModel, interpretString);

        return output;
    }

    static List<int> GetNegativeWordsList(WordModel wordModel, InterpretString interpretString)
    {
        List<int> output = GetAllIndexes(wordModel);
        foreach (string str in interpretString.NegativeWords)
        {
            if (wordModel.WordMapper.ContainsKey(str))
            {
                var commonItems = output.Intersect(wordModel.WordMapper[str]).ToList();
                foreach (var item in commonItems)
                {
                    output.RemoveAll(x => x == item);
                }
            }
        }

        return output;
    }

    static List<int> GetMainWordsList(WordModel wordModel, InterpretString interpretString)
    {
        List<int> output = GetAllIndexes(wordModel);
        if (interpretString.MainWords.Count == 0)
        {
            output.RemoveRange(0, output.Count);
        }
        else
        {
            for (int i = 0; i < interpretString.MainWords.Count; i++)
            {
                string searchString = interpretString.MainWords[i].ToUpper();
                if (wordModel.WordMapper.ContainsKey(searchString))
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

    static List<int> GetAllIndexes(WordModel wordModel)
    {
        List<int> output = new List<int>();
        for (int i = 0; i < IPathKeeper.GetListLength(wordModel.PathKeeper); i++)
        {
            output.Add(i);
        }

        return output;
    }
}