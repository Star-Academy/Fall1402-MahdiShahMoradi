using SearchText.Operands;
using SearchText.SpecialWords;

namespace SearchText;

public interface IInputHandler
{
    public void Handle(WordModel wordModel, Operand[] operands, SpecialWord[] specialWords);
}