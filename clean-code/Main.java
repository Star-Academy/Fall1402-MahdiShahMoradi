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

        FileSerializer fileSerializer = FileSerializer.createFileSerializer(PATH_TO_TEXTS);
        WordModel wordModel = WordModel.create(fileSerializer);

        System.out.println("ready to get input");

        while (true) {
            getInputPrintResult(file, scanner, wordMapper, wordsOfEachFile);
        }
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