namespace SearchText;

public interface IPathKeeper
{
    public int GetListLength();
    public string GetIthPath(int i);
    public List<string> GetPathOfCandidateIndexes(List<int> input);
    public List<int> GetAllIndexes();
}