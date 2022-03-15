
using System.IO;

namespace JsonParser.Services.Interfaces
{
    public interface IFileUtility
    {
        void DeleteFile(string file);
        FileSystemInfo[] GetFiles(string path, string searchPattern);
        string[] GetFiles(string path);
        string GetDirectory(string rootPath, string recipientDir = "", string innerLevelDir = "");

        string GetDirectory(string rootPath, string recipientDir, string innerLevelDir, string brandDir = "", string fileTypeDir = "");

        void MoveFileTo(string rootPath, string file, string recipientId = "", string innerLevelDir = "");

        void MoveFileTo(string rootPath, string file, string recipientId, string innerLevelDir, string brandDir = "", string fileTypeDir = "");

        bool FileExists(string path);
    }
}
