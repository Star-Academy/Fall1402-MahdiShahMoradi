namespace SearchText;

public class PathKeeper : IPathKeeper
{
    private readonly string _pathToFolder;
    private string[]? _pathList;
    

    public static PathKeeper Create(string path)
    {
        PathKeeper pathKeeper = new PathKeeper(path);
        pathKeeper.SetPathList();
        return pathKeeper;
    }

    private PathKeeper(string pathToFolder)
    {
        _pathToFolder = pathToFolder;
    }
    
    public int GetListLength()
    {
        return Directory.GetFiles(this._pathToFolder).Length;
    }
    
    private void SetPathList()
    {
        string[] list = new string[GetListLength()];
        for (int i = 0; i < GetListLength(); i++)
        {
            list[i] = Directory.GetFiles(this._pathToFolder)[i];
        }
        _pathList = list;
    }
    
    public string GetIthPath(int i)
    {
        return this._pathList![i];
    }
    
    public List<string> GetPathOfCandidateIndexes(List<int> input)
    {
        return input.Select(GetIthPath).ToList();
    }
    
    public List<int> GetAllIndexes()
    {
        List<int> output = new List<int>();
        for (int i = 0; i < GetListLength(); i++)
        {
            output.Add(i);
        }

        return output;
    }
}