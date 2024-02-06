import java.io.IOException;
import java.util.Scanner;

public class Main implements handleInput {

    static String PATH_TO_TEXTS = "./EnglishData/";

    public static void main(String[] args) throws IOException {

        Scanner scanner = new Scanner(System.in);
        FileSerializer fileSerializer = FileSerializer.createFileSerializer(PATH_TO_TEXTS);
        WordModel wordModel = WordModel.create(fileSerializer);
        System.out.println("ready to get input");

        while (true) {
            handleInput.printResult(wordModel, fileSerializer, scanner);
        }
    }

}