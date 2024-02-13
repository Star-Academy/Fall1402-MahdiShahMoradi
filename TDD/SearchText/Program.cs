namespace SearchText;

internal class Program
{
    private static readonly string PathToFolder = "./EnglishData";
    public static void Main(string[] args)
    {
        
        PathKeeper pathKeeper = PathKeeper.Create(PathToFolder)!;
        WordModel wordModel = WordModel.Create(pathKeeper);
        while (true)
        {
            IInputHandler.Handle(wordModel);
        }
    }
}

