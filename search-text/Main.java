import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Scanner;
import java.util.Set;

public class Main {

    static String PATH_TO_TEXTS = "./EnglishData/";

    public static void main(String[] args) throws IOException {

        Scanner scanner = new Scanner(System.in);

        File file = new File(PATH_TO_TEXTS);
        Set<String> words = new HashSet<>();
        String[] paths = file.list();

        @SuppressWarnings("unchecked")
        HashSet<String>[] wordsOfEachFile = new HashSet[paths.length];

        InitializeWords(wordsOfEachFile, paths, words);

        HashMap<String, ArrayList<Integer>> wordMapper = new HashMap<>();

        setWordMapper(words, paths.length, wordMapper, wordsOfEachFile);

        System.out.println("ready to get input");

        while (true) {
            getInputPrintResult(file, scanner, wordMapper, wordsOfEachFile);
        }
    }

    static void InitializeWords(HashSet<String>[] wordsOfEachFile, String[] paths,
            Set<String> words) throws IOException {
        for (int i = 0; i < paths.length; i++) {
            wordsOfEachFile[i] = new HashSet<>();
            List<String> lines;
            lines = Files.readAllLines(Path.of(PATH_TO_TEXTS + paths[i]));

            for (String line : lines) {
                String thisLine = line.toUpperCase();
                String[] wordsOfLine = thisLine.split("\s+");
                wordsOfEachFile[i].addAll(Arrays.asList(wordsOfLine));
                words.addAll(Arrays.asList(wordsOfLine));
            }
        }
    }

    static void setWordMapper(Set<String> words, int len, HashMap<String, ArrayList<Integer>> wordMapper,
            HashSet<String>[] wordsOfEachFile) {
        for (String word : words) {
            ArrayList<Integer> list = new ArrayList<>();
            for (int i = 0; i < len; i++) {
                if (wordsOfEachFile[i].contains(word)) {
                    list.add(i);
                }
            }
            wordMapper.put(word, list);
        }
        return;
    }

    static void printFileNames(ArrayList<Integer> list, File file) {
        if (!list.isEmpty()) {
            for (Integer i : list) {
                System.out.println(file.list()[i].toString());
            }
        } else {
            System.out.println("This words hasnt comman");
        }
        return;
    }

    static void getInputPrintResult(File file, Scanner scanner,
            HashMap<String, ArrayList<Integer>> wordMapper,
            HashSet<String>[] wordsOfEachFile) {

        ArrayList<Integer> candidateFiles = new ArrayList<>();

        for (int i = 0; i < file.list().length; i++) {
            candidateFiles.add(i);
        }

        String input = scanner.nextLine();
        String inputStringUpperCase = input.toUpperCase();
        String[] strings = inputStringUpperCase.split("\s+");
        ArrayList<String> plusStrings = new ArrayList<>();

        for (String string : strings) {
            if (string.charAt(0) == '+') {
                String stringWhitoutPlus = string.substring(1);
                plusStrings.add(stringWhitoutPlus);
            } else if (string.charAt(0) == '-') {
                String stringWhitoutMinus = string.substring(1);
                if (wordMapper.get(stringWhitoutMinus) != null)
                    candidateFiles.removeAll(wordMapper.get(stringWhitoutMinus));
            } else {
                if (wordMapper.get(string) != null)
                    candidateFiles.retainAll(wordMapper.get(string));
                else {
                    candidateFiles.removeAll(candidateFiles);
                }
            }
        }

        ArrayList<Integer> filesIsntInAnyPlus = new ArrayList<>(candidateFiles);

        if (!plusStrings.isEmpty()) {
            for (String string : plusStrings) {
                filesIsntInAnyPlus.removeAll(wordMapper.get(string));
            }
            candidateFiles.removeAll(filesIsntInAnyPlus);
        }
        printFileNames(candidateFiles, file);
    }

}