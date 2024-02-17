using System.Net;
using FluentAssertions;
using NSubstitute;
using SearchText;
using SearchText.Operands;
using SearchText.SpecialWords;

namespace SearchTextTest;

public class InputHandlerTests
{
    private static Operand[] Operands => [new And(), new Or(), new Not()];
    private static readonly SpecialWord[] Specials = [new Main(), new Negative(), new Plus()];

    private readonly string _pathToFolder = "./../../../../EnglishData";
    private readonly IInputHandler _sut;
    private readonly IInputOutput _iio;

    public InputHandlerTests()
    {
        _iio = Substitute.For<IInputOutput>();
        _sut = new InputHandler(_iio);
    }

    [Fact]
    public void CheckAllIndexGetter()
    {
        var pathKeeper = PathKeeper.Create(_pathToFolder);
        List<int> exceptedList = new List<int>();
        for (int i = 0; i < 1000; i++)
        {
            exceptedList.Add(i);
        }

        Assert.Equal(exceptedList, pathKeeper.GetAllIndexes());
    }

    [Fact]
    public void CheckMainWordList()
    {
        string text = "+>>    When I was a kid in primary +school, -mahdi";

        // 58056 => list[14]
        InterpretString interpretString = InterpretString.Create(text, Specials);
        WordModel wordModel = WordModel.Create(PathKeeper.Create(_pathToFolder));
        And and = new And();
        List<int> actual = and.GetIndexesContainsWord(wordModel, interpretString.Specials[0].Words);
        Assert.Equal([5, 14, 15], actual);
        Assert.NotEqual(wordModel.PathKeeper.GetListLength(), actual.Count);
    }

    [Fact]
    public void MainWordsDontContainAllIndexes()
    {
        string text = "+>>    When I was a kid in primary +school, -mahdi";

        // 58056 => list[14]
        InterpretString interpretString = InterpretString.Create(text, Specials);
        WordModel wordModel = WordModel.Create(PathKeeper.Create(_pathToFolder));
        And and = new And();
        List<int> actual = and.GetIndexesContainsWord(wordModel, interpretString.Specials[0].Words);
        Assert.NotEqual(wordModel.PathKeeper.GetListLength(), actual.Count);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(14)]
    [InlineData(15)]
    public void CheckNegativeWordList(int index)
    {
        InterpretString interpretString =
            InterpretString.Create("+>>    When I was a kid in primary +school, -mahdi -sick", Specials);
        WordModel wordModel = WordModel.Create(PathKeeper.Create(_pathToFolder));
        var not = new Not();
        List<int> actual = not.GetIndexesContainsWord(wordModel, interpretString.Specials[1].Words);
        Assert.Contains(index, actual);
    }

    [Fact]
    public void NegativeWordsDOntContainsAllIndexes()
    {
        InterpretString interpretString =
            InterpretString.Create("+>>    When I was a kid in primary +school, -mahdi -sick", Specials);
        WordModel wordModel = WordModel.Create(PathKeeper.Create(_pathToFolder));
        var not = new Not();
        List<int> actual = not.GetIndexesContainsWord(wordModel, interpretString.Specials[1].Words);
        Assert.NotEqual(wordModel.PathKeeper.GetListLength(), actual.Count);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(14)]
    [InlineData(15)]
    public void CheckPlusWordList(int index)
    {
        string text = "+>>    When I was a kid in primary +school, -mahdi";
        InterpretString interpretString = InterpretString.Create(text, Specials);
        WordModel wordModel = WordModel.Create(PathKeeper.Create(_pathToFolder));
        var or = new Or();
        List<int> actual = or.GetIndexesContainsWord(wordModel, interpretString.Specials[2].Words);
        Assert.Contains(index, actual);
        Assert.NotEqual(wordModel.PathKeeper.GetListLength(), actual.Count);
    }

    [Fact]
    public void PlusWordsDontContainAllIndexes()
    {
        string text = "+>>    When I was a kid in primary +school, -mahdi";
        InterpretString interpretString = InterpretString.Create(text, Specials);
        WordModel wordModel = WordModel.Create(PathKeeper.Create(_pathToFolder));
        var or = new Or();
        List<int> actual = or.GetIndexesContainsWord(wordModel, interpretString.Specials[2].Words);
        Assert.NotEqual(wordModel.PathKeeper.GetListLength(), actual.Count);
    }

    [Theory]
    [InlineData("hello -hello")]
    [InlineData("-hello +hello")]
    public void CheckPlusAndMainAndNegativeTogether(string input)
    {
        InterpretString interpretString = InterpretString.Create(input, Specials);
        WordModel wordModel = WordModel.Create(PathKeeper.Create(_pathToFolder));
        IInputHandler handler = new InputHandler(new TerminalManager());
        List<int> actual = ((InputHandler)handler).GetUnionList(wordModel, interpretString, Operands);
        Assert.Empty(actual);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(14)]
    [InlineData(15)]
    public void CheckUnionList(int excepted)
    {
        string text = "When I was +school, -mahdi";
        InterpretString interpretString = InterpretString.Create(text, Specials);
        WordModel wordModel = WordModel.Create(PathKeeper.Create(_pathToFolder));
        IInputHandler handler = new InputHandler(new TerminalManager());
        List<int> actual = ((InputHandler)handler).GetUnionList(wordModel, interpretString, Operands);
        Assert.Contains(excepted, actual);
    }

    [Fact]
    public void DontContainAllIndexes()
    {
        string text = "+>>    When I was a kid in primary +school, -mahdi";
        InterpretString interpretString = InterpretString.Create(text, Specials);
        WordModel wordModel = WordModel.Create(PathKeeper.Create(_pathToFolder));
        IInputHandler handler = new InputHandler(new TerminalManager());
        List<int> actual = ((InputHandler)handler).GetUnionList(wordModel, interpretString, Operands);
        Assert.NotEqual(wordModel.PathKeeper.GetListLength(), actual.Count);
    }

    [Fact]
    public void CheckIndexToPath()
    {
        List<int> input = [1, 3, 5, 7, 9];
        List<string> excepted =
        [
            "./../../../../EnglishData/58043",
            "./../../../../EnglishData/58045", "./../../../../EnglishData/58047",
            "./../../../../EnglishData/58049", "./../../../../EnglishData/58051"
        ];
        var actual = PathKeeper.Create(_pathToFolder).GetPathOfCandidateIndexes(input);
        actual.Should().BeEquivalentTo(excepted);
    }

    [Fact]
    public void Handle_ShouldReturnCorrectResult_WhenGivenAQeury()
    {
        // Arrange
        var wordModel = WordModel.Create(PathKeeper.Create(_pathToFolder));
        _iio.GetInput().Returns("+>>    When I was a kid in primary +school, -mahdi");
        List<string> excepted =
        [
            "./../../../../EnglishData/58047",
            "./../../../../EnglishData/58056", "./../../../../EnglishData/58057"
        ];

        // act
        _sut.Handle(wordModel, Operands, Specials);

        // assert
        _iio.Received().PrintList(Arg.Is<List<string>>(o => o.SequenceEqual(excepted)));
    }
}