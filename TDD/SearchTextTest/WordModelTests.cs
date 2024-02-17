using SearchText;

namespace SearchTextTest;

public class WordModelTests
{
    private const string PathToFolder = "./../../../../EnglishData";
    private const string PathToFirstFile = "/58056";
    [Fact]
    public void TextFileEquality()
    {
        TextOfFile textOfFile = TextOfFile.Create(PathToFolder + PathToFirstFile);
        string excepted =
            ">>    When I was a kid in primary school, I used to drink tons of milk withou> any problems." +
            "  However, nowadays, I can hardly drink any at all withou> experiencing some discomfort." +
            "  What could be responsible for the change>> Ho Leung N> ng4@husc.harvard.ed";
        Assert.Equal(excepted.ToUpper(), textOfFile.Text);
    }

    [Fact]
    public void CheckWordHashSetLength()
    {
        TextOfFile textOfFile = TextOfFile.Create(PathToFolder + PathToFirstFile);
        Assert.Equal(38, textOfFile.WordStore!.Count);
    }

    [Fact]
    public void CheckWordHashSetCorrectivity()
    {
        TextOfFile textOfFile = TextOfFile.Create(PathToFolder + PathToFirstFile);
        Assert.Contains("CAN", textOfFile.WordStore!);
    }

    [Fact]
    public void CheckWordMapper()
    {
        WordModel wordModel = WordModel.Create(PathKeeper.Create(PathToFolder)!);
        Assert.NotNull(wordModel.WordMapper["HELLO"]);
    }
}