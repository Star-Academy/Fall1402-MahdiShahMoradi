import java.util.ArrayList;

public class InputInterpret {
    private String line;
    private ArrayList<String> plusWords;
    private ArrayList<String> negativeWords;
    private ArrayList<String> mainWords;

    public InputInterpret(String line) {
        this.line = line;
    }

    static InputInterpret create(String inputString) {
        InputInterpret inputInterpreter = new InputInterpret(inputString);
        inputInterpreter.Initialize();
        inputInterpreter.setWords();
        return inputInterpreter;
    }

    public void setWords() {
        String[] words = getNormalWordsFromline(line);

        for (String string : words) {
            if (string.charAt(0) == '+') {
                String stringWhitoutPlus = string.substring(1);
                addToPlusWords(stringWhitoutPlus);
            } else if (string.charAt(0) == '-') {
                String stringWhitoutMinus = string.substring(1);
                addToNegativeWords(stringWhitoutMinus);
            } else {
                addToMainWords(string);
            }
        }
    }

    private String[] getNormalWordsFromline(String line) {
        String inputStringUpperCase = line.toUpperCase();
        String[] words = inputStringUpperCase.split("\s+");
        return words;
    }

    private void Initialize() {
        plusWords = new ArrayList<>();
        negativeWords = new ArrayList<>();
        mainWords = new ArrayList<>();
    }

    public ArrayList<String> getMainWords() {
        return mainWords;
    }

    public ArrayList<String> getPlusWords() {
        return plusWords;
    }

    public ArrayList<String> getNegativeWords() {
        return negativeWords;
    }

    public void addToNegativeWords(String word) {
        getNegativeWords().add(word);
    }

    public void addToPlusWords(String word) {
        getPlusWords().add(word);
    }

    public void addToMainWords(String word) {
        getMainWords().add(word);
    }

}
