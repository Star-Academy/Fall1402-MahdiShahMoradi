import java.io.File;
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

   public static void main(String[] args) {
      try {

        @SuppressWarnings("resource")
        Scanner scanner = new Scanner(System.in);

        File file = new File("./EnglishData");
        Set<String> words = new HashSet<>();
        String[] paths = file.list();
        
        @SuppressWarnings("unchecked")
        HashSet<String>[] wordsOfEachFile = new HashSet[paths.length];

        for(int i = 0; i < paths.length; i++) {
            wordsOfEachFile[i] = new HashSet<>();
            List<String> lines = Files.readAllLines(Path.of("./EnglishData/" + paths[i]));
            for (String line:lines) {
                String thisLine = line.toUpperCase();
                String[] wordsOfLine = thisLine.split("\s+");
                wordsOfEachFile[i].addAll(Arrays.asList(wordsOfLine));
                words.addAll(Arrays.asList(wordsOfLine));
            }
        }

        HashMap<String, ArrayList<Integer>> wordMapper = new HashMap<>();

        for(String word:words) {
            ArrayList<Integer> list = new ArrayList<>();
            for(int i = 0; i < paths.length; i++) {
                if (wordsOfEachFile[i].contains(word)) {
                    list.add(i);
                }
            }
            wordMapper.put(word, list);
        }

        System.out.println("ready to get input");

        while (true) {
            String input = scanner.nextLine();
            String inputStringUpperCase = input.toUpperCase();
            if (wordMapper.containsKey(inputStringUpperCase)) {
                for(Integer i:wordMapper.get(inputStringUpperCase)) {
                    System.out.println(file.list()[i].toString());
                }
            } else {
                System.out.println(input + " is not in files");
            }
        }
    } catch (Exception e) {
         e.printStackTrace();
    }
    }
}