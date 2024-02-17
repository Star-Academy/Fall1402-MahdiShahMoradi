using SearchText.Operands;
using SearchText.SpecialWords;
using static SearchText.IInputHandler;

namespace SearchText;

internal class Program
{
    private static readonly string PathToFolder = "./../../../../EnglishData";
    private static Operand[] Operands => [new And(), new Or(), new Not()];
    private static readonly SpecialWord[] Specials = [new Main(), new Negative(), new Plus()];

    public static void Main(string[] args)
    {
        PathKeeper pathKeeper = PathKeeper.Create(PathToFolder);
        WordModel wordModel = WordModel.Create(pathKeeper);
        IInputHandler handler = new InputHandler(new TerminalManager());
        while (true)
        {
            handler.Handle(wordModel, Operands, Specials);
        }
    }
}