namespace SearchText;

public interface IWordModel
{
    static void SetWordsOfEachFile(WordModel wordModel)
    {
        wordModel.WordsOfEachFile = new TextOfFile[IPathKeeper.GetListLength(wordModel.PathKeeper)];
        for (int i = 0; i < wordModel.WordsOfEachFile.Length; i++)
        {
            wordModel.WordsOfEachFile[i] = TextOfFile.Create(IPathKeeper.GetIthPath(wordModel.PathKeeper, i));
        }
    }

    static void SetWordStore(WordModel wordModel)
    {
        wordModel.WordStore = new HashSet<string>();
        for (int i = 0; i < IPathKeeper.GetListLength(wordModel.PathKeeper); i++)
        {
            wordModel.WordStore.UnionWith(wordModel.WordsOfEachFile![i].WordStore!);
        }
    }

    static void SetWordMapper(WordModel wordModel)
    {
        wordModel.WordMapper = new Dictionary<string, List<int>>();
        foreach (string str in wordModel.WordStore!)
        {
            List<int> strContainer = new List<int>();
            int max = IPathKeeper.GetListLength(wordModel.PathKeeper);
            for (int i = 0; i < max; i++)
            {
                if (wordModel.WordsOfEachFile![i].WordStore!.Contains(str))
                {
                    strContainer.Add(i);
                }
            }
            wordModel.WordMapper.Add(str, strContainer);
        }
    }
}