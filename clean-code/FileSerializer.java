import java.io.File;
import java.nio.file.Path;
import java.util.ArrayList;

public class FileSerializer {
    final private String PATH_TO_TEXTS;
    
    private File file;



    static FileSerializer createFileSerializer(String path_to_text) {
        FileSerializer fileSerializer = new FileSerializer(path_to_text);
        fileSerializer.setFile(new File(fileSerializer.getPATH_TO_TEXTS()));
        return fileSerializer;
    }

    public FileSerializer(String path_to_text) {
        this.PATH_TO_TEXTS = path_to_text;
    }

    public void setFile(File file) {
        this.file = file;
    }

    public String getPATH_TO_TEXTS() {
        return PATH_TO_TEXTS;
    }

    public File getFile() {
        return file;
    }

    public Integer[] getSerializedFilesArray() {
        Integer[] output = new Integer[getFileListLength()];
        for (int i = 0; i < output.length; i++) {
            output[i] = i;
        }
        return output;
    }

    public String[] getPathList() {
        return getFile().list();
    }

    public int getFileListLength() {
        return getFile().list().length;
    }

    public Path getIthFilePath(int i) {
        return Path.of(PATH_TO_TEXTS + getPathList()[i]);
    }

    public ArrayList<String> getDeseralizedFiles(ArrayList<Integer> input) {

        ArrayList<String> output = new ArrayList<>();

        for (Integer i : input) {
            output.add(getIthFilePath(i).toString());
        }

        return output;
    }
}
