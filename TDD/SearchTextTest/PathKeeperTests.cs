using SearchText;

namespace SearchTextTest;

public class FileSerializerTests
{
    [Fact]
    public void SerializeDeserializeCorrectivity()
    {
        string path = "./../../../../EnglishData";
        FileSerializer fileSerializer = FileSerializer.Create(path);
        string excepted = path + "\\57110";
        Assert.Equal(excepted, fileSerializer.PathList![0]);
    }

    [Fact]
    public void GetListLengthCheck()
    {
        string path = "./../../../../EnglishData";
        FileSerializer fileSerializer = FileSerializer.Create(path);
        Assert.Equal(1000, IFileSerializer.GetListLength(fileSerializer));
    }

    [Fact]
    public void GetIthPathCheck()
    {
        string path = "./../../../../EnglishData";
        FileSerializer fileSerializer = FileSerializer.Create(path);
        Assert.Equal(path + "\\58052", IFileSerializer.GetIthPath(fileSerializer, 10));
    }
}


