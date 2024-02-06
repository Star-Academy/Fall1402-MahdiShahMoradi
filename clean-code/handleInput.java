import java.util.ArrayList;
import java.util.Scanner;

public interface handleInput {

    static ArrayList<String> getCandidateFiles(WordModel wordModel,
            FileSerializer fileSerializer, Scanner scanner) {

        InputInterpret inputInterpret = InputInterpret.create(scanner.nextLine());
        ArrayList<Integer> serializedOutput = unionFiles(inputInterpret, wordModel);
        
        ArrayList<String> output = fileSerializer.getDeseralizedFiles(serializedOutput);
        return output;
    }

    public static void printResult(WordModel wordModel,
            FileSerializer fileSerializer, Scanner scanner) {

        ArrayList<String> outputStrings = getCandidateFiles(wordModel, fileSerializer, scanner);

        if (!outputStrings.isEmpty()) {
            for (String string : outputStrings) {
                System.out.println(string);
            }
        } else {
            System.out.println("Nothing find");
        }
    }

    private static ArrayList<Integer> unionFiles(InputInterpret inputInterpret, WordModel wordModel) {

        ArrayList<Integer> output = getTemplateOfCandidateFiles(wordModel);
        ArrayList<Integer> plusStringsFiles = getFilesOfPlusStrings(inputInterpret, wordModel);
        ArrayList<Integer> mainStringsFiles = getFilesOfMainStrings(inputInterpret, wordModel);
        ArrayList<Integer> negativeStringsFiles = getFilesOfNegativeStrings(inputInterpret, wordModel);

        System.out.println(plusStringsFiles.size() + " " + negativeStringsFiles.size() + " " + mainStringsFiles.size());

        output.retainAll(plusStringsFiles);
        System.out.println(output.size());

        output.retainAll(mainStringsFiles);
        System.out.println(output.size());

        output.retainAll(negativeStringsFiles);
        System.out.println(output.size());
        return output;
    }

    private static ArrayList<Integer> getFilesOfPlusStrings(InputInterpret inputInterpret, WordModel wordModel) {

        ArrayList<Integer> output = getTemplateOfCandidateFiles(wordModel);
        ArrayList<Integer> filesIsntInAnyPlus = getTemplateOfCandidateFiles(wordModel);

        if (!inputInterpret.getPlusWords().isEmpty()) {
            for (String string : inputInterpret.getPlusWords()) {
                filesIsntInAnyPlus.removeAll(wordModel.getWordMapper().get(string));
            }
        } else {
            filesIsntInAnyPlus.removeAll(filesIsntInAnyPlus);
        }
        output.removeAll(filesIsntInAnyPlus);
        return output;
    }

    private static ArrayList<Integer> getFilesOfMainStrings(InputInterpret inputInterpret, WordModel wordModel) {

        ArrayList<Integer> output = getTemplateOfCandidateFiles(wordModel);

        for (String string : inputInterpret.getMainWords()) {

            if (wordModel.getWordMapper().get(string) != null)
                output.retainAll(wordModel.getWordMapper().get(string));
            else {
                output.removeAll(output);
                return output;
            }
        }

        return output;
    }

    private static ArrayList<Integer> getTemplateOfCandidateFiles(WordModel wordModel) {

        ArrayList<Integer> candidateFiles = new ArrayList<>();

        for (int i = 0; i < wordModel.getListLentgh(); i++) {
            candidateFiles.add(i);
        }

        return candidateFiles;
    }

    private static ArrayList<Integer> getFilesOfNegativeStrings(InputInterpret inputInterpret, WordModel wordModel) {

        ArrayList<Integer> output = getTemplateOfCandidateFiles(wordModel);

        for (String string : inputInterpret.getNegativeWords()) {
            if (wordModel.getWordMapper().get(string) != null) {
                output.removeAll(wordModel.getWordMapper().get(string));
            }
        }

        return output;
    }

}
