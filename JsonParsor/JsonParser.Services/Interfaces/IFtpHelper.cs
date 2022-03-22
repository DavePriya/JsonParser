
namespace JsonParser.Services.Interfaces
{
    public interface IFtpHelper
    {
        void DownloadSFTPFiles(string host, string ftpUser, string ftpPwd, string sftpDir, string downloadPath, bool deleteFileAfterDownload);
         }
}
