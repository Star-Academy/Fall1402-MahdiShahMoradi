using SearchText;

namespace SearchTextTest;

public class InterpretStringTests
{
    [Fact]
    public void CheckSplitWords()
    {
        InterpretString interpretString = InterpretString.Create("1 2 3 +4 -5 6 -7 +8 9 10");
        string[] mainWords = new[] { "1", "2", "3", "6", "9", "10" };
        string[] negativeWords = new[] { "4", "8" };
        string[] plusWords = ["5", "7"];
        
        Assert.Equal(6, interpretString.MainWords.Count);
        Assert.Equal(2, interpretString.PlusWords.Count);
        Assert.Equal(2, interpretString.NegativeWords.Count);
    }

    [Fact]
    public void CheckInterpretWords()
    {
        InterpretString interpretString = InterpretString.Create("1 2 3 +4 -5 6 -7 +8 9 10");
        string[] mainWords = ["1", "2", "3", "6", "9", "10"];
        string[] plusWords = ["4", "8"];
        string[] negativeWords = ["5", "7"];
        
        Assert.True(interpretString.MainWords.Select(x => x)
            .Intersect(mainWords)
            .Any());
        Assert.True(interpretString.NegativeWords.Select(x => x)
            .Intersect(negativeWords)
            .Any());
        Assert.True(interpretString.PlusWords.Select(x => x)
            .Intersect(plusWords)
            .Any());
    }
}