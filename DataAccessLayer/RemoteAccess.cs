using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
namespace InsuranceCalculator.DataAccessLayer
{
    public class RemoteAccess
    {
        public static string RemotePathPrefix = $"https://vijayapps.s3-us-west-1.amazonaws.com/clients/pic/";
        public static string ApplicationName = "pic";

        public static XElement GetXmlFromServer(string fileName)
        {
            string downloadUrl = RemotePathPrefix + $"{fileName}";

            string tempFileName = Download(downloadUrl, $"{fileName}");

            XElement fileElement = XElement.Load(tempFileName);

            return fileElement;
        }
        public static XElement GetXmlFromServer(string folder, string fileName)
        {
            string downloadUrl = RemotePathPrefix + $"{folder}/{fileName}";

            string tempFileName = Download(downloadUrl, $"{fileName}");

            XElement fileElement = XElement.Load(tempFileName);

            return fileElement;
        }
        public static string GetFileFromServer(string folder, string fileName)
        {
            string downloadUrl = RemotePathPrefix + $"{folder}/{fileName}";

            string tempFileName = Download(downloadUrl, $"{fileName}");

            return tempFileName;
        }
        public static void PutToServer(string fileName)
        {
            string fName = Path.GetFileName(fileName);
            string uploadURL = RemotePathPrefix + $"{fName}";

            Upload(uploadURL, fileName);
        }
        public static void PutToServerWithNewName(string folder, string fileName, string newFileName)
        {
            string uploadURL = RemotePathPrefix + $"{folder}/{newFileName}";

            Upload(uploadURL, fileName);
        }
        public static void PutToServer(string folder, string fileName)
        {
            string fName = Path.GetFileName(fileName);
            string uploadURL = RemotePathPrefix + $"{folder}/{fName}";

            Upload(uploadURL, fileName);
        }
        private static string Download(string downloadURL, string fileName)
        {
            string folderPath = CommonFunctions.GetTempPath();
            string downloadPath = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(downloadURL, downloadPath);
                }

                return downloadPath;
            }
            catch (Exception ex)
            {
                //CommonFunctions.ShowErrorMessage(ex.Message);
                throw;
            }
        }

        private static void Upload(string uploadURL, string fileName)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                using (WebClient client = new WebClient())
                {
                    client.UploadFile(uploadURL, "PUT", fileName);
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.ShowErrorMessage(ex.Message);
                throw;
            }
        }
    }
}
