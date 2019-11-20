namespace RecursionSolver
{
    public interface IFileOperations
    {
        void DeleteFile(string filePath);
        void RenameFile(string filePath);
        void ModifyFile(string filePath);
        void CloneFile(string filePath);
    }
}
