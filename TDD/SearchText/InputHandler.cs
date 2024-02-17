using SearchText.Operands;
using SearchText.SpecialWords;

namespace SearchText;

public class InputHandler(IInputOutput iio) : IInputHandler
{
    public void Handle(WordModel wordModel, Operand[] operands, SpecialWord[] specialWords)
    {
        MakeSpecialWordsNew(specialWords);
        var line = iio.GetInput();
        var interpretString = InterpretString.Create(line, specialWords);
        var candidateIndexes = GetUnionList(wordModel, interpretString, operands);
        var candidatePaths = wordModel.PathKeeper.GetPathOfCandidateIndexes(candidateIndexes);
        iio.PrintList(candidatePaths);
    }

    private void MakeSpecialWordsNew(SpecialWord[] specialWords)
    {
        foreach (var special in specialWords)
        {
            special.Words = [];
        }
    }

    public List<int> GetUnionList(WordModel wordModel, InterpretString interpretString, Operand[] operands)
    {
        var output = operands[0].GetIndexesContainsWord(wordModel, interpretString.Specials[0].Words);
        foreach (var operand in operands)
        {
            foreach (var special in interpretString.Specials)
            {
                if (operand.Sign != special.Sign) continue;
                var containers = operand.GetIndexesContainsWord(wordModel, special.Words);
                output = output.Intersect(containers).ToList();
                break;
            }
        }
        return output;
    }
}