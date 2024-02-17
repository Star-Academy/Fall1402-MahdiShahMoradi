using FluentAssertions;
using SearchText;

namespace SearchTextTest;

public class PathKeeperTests
{
    [Fact]
    public void GetListLengthCheck()
    {
        string path = "./../../../../EnglishData";
        PathKeeper pathKeeper = PathKeeper.Create(path);
        Assert.Equal(1000, pathKeeper.GetListLength());
    }

    [Fact]
    public void GetIthPathCheck()
    {
        string path = "./../../../../EnglishData";
        PathKeeper pathKeeper = PathKeeper.Create(path);
        Assert.Equal(path + "/58052", pathKeeper.GetIthPath(10));
    }
}


