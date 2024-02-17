using SearchText;
using SearchText.SpecialWords;

namespace SearchTextTest;

public class InterpretStringTests
{
    private static readonly SpecialWord[] Specials = [new Main(), new Negative(), new Plus()];
    [Theory]
    [InlineData(6, 0)]
    [InlineData( 2, 1)]
    [InlineData(2, 2)]
    public void CheckSplitWords(int answer, int index)
    {
        MakeSpecialWordsNew(Specials);
        var count = InterpretString.Create("1 2 3 +4 -5 6 -7 +8 9 10", Specials).Specials[index].Words.Count;

       Assert.Equal(answer, count);
    }

    [Theory]
    [InlineData(0, new[] {"1", "2", "3", "6", "9", "10"} )]
    [InlineData(2, new[] {"4", "8"} )]
    [InlineData(1, new[] {"5", "7"} )]
    public void CheckInterpretWords(int index, string[] words)
    {
        MakeSpecialWordsNew(Specials);
        var interpretString = InterpretString.Create("1 2 3 +4 -5 6 -7 +8 9 10", Specials);
        
        Assert.True(interpretString.Specials[index].Words.Select(x => x)
            .Intersect(words)
            .Any());
    }
    
    public void MakeSpecialWordsNew(SpecialWord[] specialWords)
    {
        foreach (var special in specialWords)
        {
            special.Words = [];
        }
    }
}