using JsonParser.Services.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace JsonParser.Services.Implementations
{
    public class FileUtility : IFileUtility
    {
        public string GetDirectory(string rootPath, string recipientDir = "", string innerLevelDir = "")
        {
            var createdDir = Directory.CreateDirectory(rootPath);
            if (!string.IsNullOrEmpty(recipientDir))
            {
                createdDir = createdDir.CreateSubdirectory(recipientDir);
                if (!string.IsNullOrEmpty(innerLevelDir))
                {
                    createdDir = createdDir.CreateSubdirectory(innerLevelDir);
                    createdDir = createdDir.CreateSubdirectory(DateTime.Today.Year.ToString());
                    createdDir = createdDir.CreateSubdirectory(DateTime.Today.Month.ToString());
                }
            }
            return createdDir.FullName;
        }

        public string GetDirectory(string rootPath, string recipientDir, string innerLevelDir, string brandDir = "", string fileTypeDir = "")
        {
            var createdDir = Directory.CreateDirectory(rootPath);
            if (!string.IsNullOrEmpty(recipientDir))
            {
                createdDir = createdDir.CreateSubdirectory(recipientDir);

                if (!string.IsNullOrEmpty(brandDir))
                {
                    createdDir = createdDir.CreateSubdirectory(brandDir);
                    if (!string.IsNullOrEmpty(fileTypeDir))
                    {
                        createdDir = createdDir.CreateSubdirectory(fileTypeDir);
                    }
                    if (!string.IsNullOrEmpty(innerLevelDir))
                    {
                        createdDir = createdDir.CreateSubdirectory(innerLevelDir);
                        createdDir = createdDir.CreateSubdirectory(DateTime.Today.Year.ToString());
                        createdDir = createdDir.CreateSubdirectory(DateTime.Today.Month.ToString());
                    }
                }
            }
            return createdDir.FullName;
        }

        public void MoveFileTo(string rootPath, string file, string recipientId = "", string innerLevelDir = "")
        {
            if (File.Exists(file))
            {
                var dirPath = GetDirectory(rootPath, recipientId, innerLevelDir);
                string destPath = Path.Combine(dirPath, Path.GetFileName(file));
                if (File.Exists(destPath))
                {
                    File.Delete(destPath);
                }
                File.Move(file, destPath);
            }
        }

        public void MoveFileTo(string rootPath, string file, string recipientId, string innerLevelDir, string brandDir = "", string fileTypeDir = "")
        {
            if (File.Exists(file))
            {
                var dirPath = GetDirectory(rootPath, recipientId, innerLevelDir, brandDir, fileTypeDir);
                string destPath = Path.Combine(dirPath, Path.GetFileName(file));
                if (File.Exists(destPath))
                {
                    File.Delete(destPath);
                }
                File.Move(file, destPath);
            }
        }

        public void DeleteFile(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }
        public FileSystemInfo[] GetFiles(string path, string searchPattern)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            FileSystemInfo[] files = di.GetFileSystemInfos(searchPattern);
            return files.OrderBy(f => f.CreationTime).ToArray();
        }

        public string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        public bool FileExists(string path)
        {
            if (File.Exists(path))
                return true;
            else
                return false;
        }
    }
}
