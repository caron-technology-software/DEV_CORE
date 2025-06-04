using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentFTP;

namespace ProRob.Net
{
    //https://github.com/robinrodricks/FluentFTP/tree/master/FluentFTP.Examples

    public class Ftp
    {
        string IpAddress { get; set; }
        string Username { get; set; }
        string Password { get; set; }

        public Ftp(string ipAddress, string username = "anonymous", string password = "anonymous")
        {
            IpAddress = ipAddress;
            Username = username;
            Password = password;
        }

        public bool UploadData(string data, string remotePath, bool overwriteIfExist = false)
        {
            try
            {
                using var ftp = new FtpClient(IpAddress, Username, Password);
                ftp.Connect();

                if (!ftp.IsConnected)
                {
                    return false;
                }

                var existMode = overwriteIfExist ? FtpRemoteExists.Overwrite : FtpRemoteExists.Skip;

                var ms = new MemoryStream(Encoding.ASCII.GetBytes(data));

                FtpStatus status = ftp.UploadStream(ms, remotePath, existMode);

                ms.Dispose();

                return (status == FtpStatus.Success);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProRob.Net.Ftp] Exception on UploadData\tMessage: {ex.Message}");
                return false;
            }
        }

        public bool UploadFile(string localPath, string remotePath, bool overwriteIfExist = false)
        {
            try
            {
                using var ftp = new FtpClient(IpAddress, Username, Password);

                ftp.Connect();

                if (!ftp.IsConnected)
                {
                    return false;
                }

                var existMode = overwriteIfExist ? FtpRemoteExists.Overwrite : FtpRemoteExists.Skip;

                FtpStatus status = ftp.UploadFile(localPath, remotePath, existMode);

                return (status == FtpStatus.Success);
            }
            catch
            {
                Console.WriteLine("[ProRob.Net.Ftp] Exception on UploadFile()");
                return false;
            }
        }

        public bool CreateDirectory(string path)
        {
            if (DirectoryExists(path))
            {
                return false;
            }

            try
            {
                using (var ftp = new FtpClient(IpAddress, Username, Password))
                {
                    ftp.Connect();

                    if (!ftp.IsConnected)
                    {
                        return false;
                    }

                    return ftp.CreateDirectory(path);
                }
            }
            catch
            {
                Console.WriteLine("[ProRob.Net.Ftp] Exception on CreateDirectory()");
                return false;
            }
        }

        public bool DirectoryExists(string path)
        {
            try
            {
                using (var ftp = new FtpClient(IpAddress, Username, Password))
                {
                    ftp.Connect();

                    if (!ftp.IsConnected)
                    {
                        return false;
                    }

                    return ftp.DirectoryExists(path);
                }
            }
            catch
            {
                Console.WriteLine("[ProRob.Net.Ftp] Exception on DirectoryExists()");
                return false;
            }
        }

        public bool FileExists(string path)
        {
            try
            {
                using var ftp = new FtpClient(IpAddress, Username, Password);
                ftp.Connect();

                if (!ftp.IsConnected)
                {
                    return false;
                }

                return ftp.FileExists(path);
            }
            catch
            {
                Console.WriteLine("[ProRob.Net.Ftp] Exception on FileExists()");
                return false;
            }
        }
    }
}