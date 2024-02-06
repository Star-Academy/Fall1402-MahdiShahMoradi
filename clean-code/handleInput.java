import java.util.ArrayList;
import java.util.Scanner;

public interface handleInput {

    static ArrayList<Integer> getCandidateWords(WordModel wordModel,
             FileSerializer fileSerializer, Scanner scanner) {

                
        ArrayList<Integer> candidateFiles = new ArrayList<>();

        initializeCandidateFiles(candidateFiles, wordModel);

        InputInterpret inputInterpret = InputInterpret.create(scanner.nextLine());

        for (String string:inputInterpret.getMainWords()) {
            if (wordModel.getWordMapper().get(string) != null)
                candidateFiles.retainAll(wordModel.getWordMapper().get(string));
            else {
                candidateFiles.removeAll(candidateFiles);
            }
        }



        for (String string :inputInterpret.getNegativeWords()) {
            if (wordModel.getWordMapper().get(string) != null) {
                    candidateFiles.removeAll(wordModel.getWordMapper().get(string));
            }
        }

        ArrayList<String> plusStrings = new ArrayList<>();

        if (!inputInterpret.getPlusWords().isEmpty()) {
            for (String string : plusStrings) {
                filesIsntInAnyPlus.removeAll(wordMapper.get(string));
            }
            candidateFiles.removeAll(filesIsntInAnyPlus);
        }

        return null;
    }

    private void setCandidateFilesOfMainWords() {
        
    }

    private static void initializeCandidateFiles(ArrayList<Integer> candidateFiles, WordModel wordModel) {

        for (int i = 0; i < wordModel.getListLentgh(); i++) {
            candidateFiles.add(i);
        }
    }
}
