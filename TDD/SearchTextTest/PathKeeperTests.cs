using SearchText;

namespace SearchTextTest;

public class PathKeeperTests
{
    [Fact]
    public void GetListLengthCheck()
    {
        string path = "./../../../../EnglishData";
        PathKeeper? pathKeeper = PathKeeper.Create(path);
        Assert.Equal(1000, IPathKeeper.GetListLength(pathKeeper));
    }

    [Fact]
    public void GetIthPathCheck()
    {
        string path = "./../../../../EnglishData";
        PathKeeper? pathKeeper = PathKeeper.Create(path);
        Assert.Equal(path + "\\58052", IPathKeeper.GetIthPath(pathKeeper, 10));
    }
}


