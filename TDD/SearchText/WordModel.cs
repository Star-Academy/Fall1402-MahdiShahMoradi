namespace SearchText;

public class WordModel
{
    public TextOfFile[]? WordsOfEachFile;
    public HashSet<string>? WordStore;
    public readonly PathKeeper PathKeeper;
    public Dictionary<string, List<int>> WordMapper;

    private WordModel(PathKeeper pathKeeper)
    {
        PathKeeper = pathKeeper;
    }

    public static WordModel Create(PathKeeper pathKeeper)
    {
        WordModel wordModel = new WordModel(pathKeeper);
        IWordModel.SetWordsOfEachFile(wordModel);
        IWordModel.SetWordStore(wordModel);
        IWordModel.SetWordMapper(wordModel);
        return wordModel;
    }
}