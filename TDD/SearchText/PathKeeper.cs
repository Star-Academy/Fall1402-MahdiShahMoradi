namespace SearchText;

public class FileSerializer
{
    public readonly string PathToFolder;
    public string[]? PathList;

    public static FileSerializer Create(string path)
    {
        FileSerializer fileSerializer = new FileSerializer(path);
        IFileSerializer.SetPathList(fileSerializer);
        return fileSerializer;
    }

    private FileSerializer(string pathToFolder)
    {
        PathToFolder = pathToFolder;
    }
}