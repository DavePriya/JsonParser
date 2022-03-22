using FluentFTP;
using JsonParser.Services.Interfaces;
using System;
using System.IO;

namespace JsonParser.Services.Implementations
{
    public class FtpHelper : IFtpHelper
    {
        public void DownloadSFTPFiles(string host, string ftpUser, string ftpPwd, string ftpDir, string downloadPath, bool deleteFileAfterDownload)
        {
            using (var ftp = new FtpClient(host, ftpUser, ftpPwd))
            {
                ftp.Connect();
                foreach (FtpListItem item in ftp.GetListing(ftpDir))
                {

                    // if this is a file
                    if (item.Type == FtpFileSystemObjectType.File && (Path.GetExtension(item.FullName) == ".json"))
                    {
                        // get the file size
                        //  long size = ftp.GetFileSize(item.FullName);

                        // get modified date/time of the file or folder
                        //DateTime time = ftp.GetModifiedTime(item.FullName);

                        // calculate a hash for the file on the server side (default algorithm)
                        // FtpHash hash = ftp.GetHash(item.FullName);

                        // download the file 
                        var fileName = Path.GetFileName(item.FullName);
                        var saveFilePath = Path.Combine(downloadPath, fileName ?? throw new InvalidOperationException("File Appears to not have a name"));

                        ftp.DownloadFile(localPath: saveFilePath, remotePath: item.FullName);
                    }
                }

                // download many files, skip if they already exist on disk
                //ftp.DownloadFiles(@"D:\Drivers\test\",
                //    new[] {
                //        @"/public_html/temp/file0.exe",
                //        @"/public_html/temp/file1.exe",
                //        @"/public_html/temp/file2.exe",
                //        @"/public_html/temp/file3.exe",
                //        @"/public_html/temp/file4.exe"
                //    }, FtpLocalExists.Skip);

                //ftp.DownloadFiles(downloadPath,new )

            }
        }
    }
}
