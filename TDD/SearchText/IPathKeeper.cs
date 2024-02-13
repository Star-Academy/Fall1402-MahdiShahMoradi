namespace SearchText;

public interface IPathKeeper
{
    public static int GetListLength(PathKeeper pathKeeper)
    {
        return Directory.GetFiles(pathKeeper.PathToFolder).Length;
    }
    
    public static void SetPathList(PathKeeper pathKeeper)
    {
        string[] list = new string[GetListLength(pathKeeper)];
        for (int i = 0; i < GetListLength(pathKeeper); i++)
        {
            list[i] = Directory.GetFiles(pathKeeper.PathToFolder)[i];
        }
        pathKeeper.PathList = list;
    }

    public static string GetIthPath(PathKeeper pathKeeper, int i)
    {
        return pathKeeper.PathList![i];
    }
}