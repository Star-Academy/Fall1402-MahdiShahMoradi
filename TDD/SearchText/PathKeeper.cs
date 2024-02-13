namespace SearchText;

public class PathKeeper
{
    public readonly string PathToFolder;
    public string[]? PathList;
    

    public static PathKeeper? Create(string path)
    {
        PathKeeper? pathKeeper = new PathKeeper(path);
        IPathKeeper.SetPathList(pathKeeper);
        return pathKeeper;
    }

    private PathKeeper(string pathToFolder)
    {
        PathToFolder = pathToFolder;
    }
}