using SearchText;

namespace SearchTextTest;

public class InputHandlerTests
{
    private readonly string _pathToFolder = "./../../../../EnglishData";
    [Fact]
    public void CheckAllIndexGetter()
    {
        WordModel wordModel = WordModel.Create(PathKeeper.Create(_pathToFolder)!);
        List<int> exceptedList = new List<int>();
        for (int i = 0; i < 1000; i++)
        {
            exceptedList.Add(i);
        }
        Assert.Equal(exceptedList, IInputHandler.GetAllIndexes(wordModel));
    }

    [Fact]
    public void CheckMainWordList()
    {
        string text =
            ">>    When I was a kid in primary school, I used to drink tons of milk withou> any problems." +
            "  However, nowadays, I can hardly drink any at all withou> experiencing some discomfort." +
            "  What could be responsible for the change>> Ho Leung N> ng4@husc.harvard.ed";
        
        // 58056 => list[14]
        InterpretString interpretString = InterpretString.Create(text);
        WordModel wordModel = WordModel.Create(PathKeeper.Create(_pathToFolder)!);
        List<int> actual = IInputHandler.GetMainWordsList(wordModel, interpretString);
        Assert.Equal([14], actual);
        Assert.NotEqual(IPathKeeper.GetListLength(wordModel.PathKeeper), actual.Count);
    }

    [Fact]
    public void CheckNegativeWordList()
    {
        InterpretString interpretString = InterpretString.Create("-WHEN -temporaryValue=$#F#$43");
        WordModel wordModel = WordModel.Create(PathKeeper.Create(_pathToFolder)!);
        List<int> actual = IInputHandler.GetNegativeWordsList(wordModel, interpretString);
        Assert.DoesNotContain(14, actual);
        Assert.NotEqual(IPathKeeper.GetListLength(wordModel.PathKeeper), actual.Count);
    }

    [Fact]
    public void CheckPlusWordList()
    {
        string text =
            "+>>    +When +I +was +a +kid +in +primary +school, +temporaryValue=@%YGR";
        InterpretString interpretString = InterpretString.Create(text);
        WordModel wordModel = WordModel.Create(PathKeeper.Create(_pathToFolder)!);
        List<int> actual = IInputHandler.GetPlusWordsList(wordModel, interpretString);
        Assert.Contains(5, actual);
        Assert.Contains(14, actual);
        Assert.Contains(15, actual);
        Assert.NotEqual(IPathKeeper.GetListLength(wordModel.PathKeeper), actual.Count);
    }

    [Theory]
    [InlineData("hello -hello")]
    [InlineData("-hello +hello")]
    public void CheckPlusAndMainAndNegativeTogethere(string input)
    {
        string text = input;
        InterpretString interpretString = InterpretString.Create(text);
        WordModel wordModel = WordModel.Create(PathKeeper.Create(_pathToFolder)!);
        List<int> actual = IInputHandler.UnionLists(wordModel, interpretString);
        Assert.Equal(0, actual.Count);
    
    }

    [Fact]
    public void CheckUnionList()
    {
        string text = "+>>    When I was a kid in primary +school, -mahdi";
        InterpretString interpretString = InterpretString.Create(text);
        WordModel wordModel = WordModel.Create(PathKeeper.Create(_pathToFolder)!);
        List<int> actual = IInputHandler.UnionLists(wordModel, interpretString);
        Assert.Contains(5, actual);
        Assert.Contains(14, actual);
        Assert.Contains(15, actual);
        Assert.NotEqual(IPathKeeper.GetListLength(wordModel.PathKeeper), actual.Count);
    }

    [Fact]
    public void CheckIndexToPath()
    {
        List<int> input = [1, 3, 5, 7, 9];
        List<string> excepted = ["./../../../../EnglishData\\58043", 
            "./../../../../EnglishData\\58045", "./../../../../EnglishData\\58047", 
            "./../../../../EnglishData\\58049", "./../../../../EnglishData\\58051"];
        Assert.Equal(excepted, 
            IInputHandler.GetPathOfCondidateIndexes(input, PathKeeper.Create(_pathToFolder)));
    }
}