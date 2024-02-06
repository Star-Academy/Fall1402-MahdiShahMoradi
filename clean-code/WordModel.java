import java.io.IOException;
import java.nio.file.Files;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

public class WordModel {

    private HashSet<String>[] wordsOfEachFile;

    private Set<String> wordStrore = new HashSet<>();
    private HashMap<String, ArrayList<Integer>> wordMapper = new HashMap<>();
    final private FileSerializer fileSerializer;

    public WordModel(FileSerializer fileSerializer) {
        this.fileSerializer = fileSerializer;
    }

    static WordModel create(FileSerializer fileSerializer) throws IOException {
        WordModel wordModel = new WordModel(fileSerializer);
        wordModel.InitializeWords();
        wordModel.setWordMapper();
        return wordModel;
    }

    private void setWordMapper() {
        for (String word : wordStrore) {
            ArrayList<Integer> list = new ArrayList<>();
            int len = getListLentgh();
            for (int i = 0; i < len; i++) {
                if (wordsOfEachFile[i].contains(word)) {
                    list.add(i);
                }
            }
            wordMapper.put(word, list);
            System.out.println(wordMapper.size());
        }
    }

    @SuppressWarnings("unchecked")
    private void InitializeWords() throws IOException {
        wordsOfEachFile = new HashSet[getListLentgh()];
        for (int i = 0; i < getListLentgh(); i++) {
            wordsOfEachFile[i] = new HashSet<>();
            List<String> lines = Files.readAllLines(fileSerializer.getIthFilePath(i));
            putWordsToWordStore(lines);
            putWordsToWordsOfEachLine(lines, i);
        }
    }

    private void putWordsToWordsOfEachLine(List<String> lines, int i) {
        for (String line : lines) {
            wordsOfEachFile[i].addAll(getWordsOfLineList(line));
        }
    }

    private void putWordsToWordStore(List<String> lines) {
        for (String line : lines) {
            wordStrore.addAll(getWordsOfLineList(line));
        }
    }

    private List<String> getWordsOfLineList(String line) {
        String thisLine = line.toUpperCase();
        String[] wordsOfLine = thisLine.split("\s+");
        return Arrays.asList(wordsOfLine);
    }

    public int getListLentgh() {
        return fileSerializer.getFileListLentgh();
    }

    public HashMap<String, ArrayList<Integer>> getWordMapper() {
        return wordMapper;
    }

}
