using log4net;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace CMEGroupFileDownaloder
{
    class FileDownload
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            try
            {
                Logger.Info(LogHelper.LogInfo(MethodBase.GetCurrentMethod()));
                FileDownload fileDownload = new FileDownload();
                fileDownload.Download(Config.Default.CMEPositionLimitFileURI, Config.Default.Destination);
                fileDownload.Download(Config.Default.CBOTPositionLimitFileURI, Config.Default.Destination);
                fileDownload.Download(Config.Default.NYMEXPositionLimitFileURI, Config.Default.Destination);
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }

        private void Download(string uri, string destination)
        {
            Logger.Info(LogHelper.LogInfo(MethodBase.GetCurrentMethod(), uri, destination));

            try
            {
                string fileName = System.IO.Path.GetFileName(uri);

                Logger.Info(fileName);

                if (!Directory.Exists(destination))
                {
                    Directory.CreateDirectory(destination);
                }

                string combined = Path.Combine(destination, fileName);

                Logger.Info(combined);

                using (var client = new WebClient())
                {
                    client.DownloadFile(uri, combined);
                }

            }
            catch (Exception exception)
            {
                Logger.Error(exception);                
            }
        }
    }
}
