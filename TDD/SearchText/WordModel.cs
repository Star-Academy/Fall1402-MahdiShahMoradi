namespace SearchText;

public class WordModel
{
    private TextOfFile[]? _wordsOfEachFile;
    private HashSet<string>? WordStore;
    public readonly IPathKeeper PathKeeper;
    public Dictionary<string, List<int>>? WordMapper;

    private WordModel(IPathKeeper pathKeeper)
    {
        PathKeeper = pathKeeper;
    }
    
    public static WordModel Create(IPathKeeper pathKeeper)
    {
        WordModel wordModel = new WordModel(pathKeeper);
        wordModel.PreProcess();
        return wordModel;
    }

    private void PreProcess()
    {
        SetWordsOfEachFile();
        SetWordStore();
        SetWordMapper();
    }

    

    private void SetWordsOfEachFile()
    {
        this._wordsOfEachFile = new TextOfFile[this.PathKeeper.GetListLength()];
        for (int i = 0; i < this._wordsOfEachFile.Length; i++)
        {
            this._wordsOfEachFile[i] = TextOfFile.Create(this.PathKeeper.GetIthPath(i));
        }
    }

    private void SetWordStore()
    {
        this.WordStore = new HashSet<string>();
        for (int i = 0; i < this.PathKeeper.GetListLength(); i++)
        {
            this.WordStore.UnionWith(this._wordsOfEachFile![i].WordStore!);
        }
    }

    private void SetWordMapper()
    {
        this.WordMapper = new Dictionary<string, List<int>>();
        foreach (string str in this.WordStore!)
        {
            List<int> strContainer = new List<int>();
            int max = this.PathKeeper.GetListLength();
            for (int i = 0; i < max; i++)
            {
                if (this._wordsOfEachFile![i].WordStore!.Contains(str))
                {
                    strContainer.Add(i);
                }
            }

            this.WordMapper.Add(str, strContainer);
        }
    }
}