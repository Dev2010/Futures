using log4net;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace CMEGroupPositionLimitFileParser
{
    class PositionLimitFileParser
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            Logger.Info(LogHelper.LogInfo(MethodBase.GetCurrentMethod()));

            PositionLimitFileParser positionLimitFileParser = new PositionLimitFileParser();

            try
            {
                IEnumerable<string> files = positionLimitFileParser.SearchFiles(Config.Default.SourceFolder, string.Empty);
                //ExcelHelper.ConvertExcelSheetToCsv(Config.Default.SourceFolder, files);
                foreach (string fileName in files)
                {
                    if (fileName.StartsWith(Config.Default.CMEPositionLimitFilePattern, true, CultureInfo.InvariantCulture))
                    {
                        CMEPositionLimitFileParser cmePositionLimitFileParser = new CMEPositionLimitFileParser(Config.Default.SourceFolder, fileName);
                        DataTable dtCME = cmePositionLimitFileParser.Read();
                        long runId = DateTime.Now.Ticks;
                        using (var conn = new SqlConnection(Config.Default.ExchangeDatabaseConnectionString))
                        {
                            conn.Open();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }

        public IEnumerable<string> SearchFiles(string folder, string filePattern)
        {
            Logger.Info(LogHelper.LogInfo(MethodBase.GetCurrentMethod(), folder, filePattern));

            if (!Directory.Exists(folder))
            {
                throw new FileNotFoundException("Folder not found", folder);
            }

            IEnumerable<string> excelFiles = from fullFilename
                                                in Directory.EnumerateFiles(folder, "*.xlsx", SearchOption.TopDirectoryOnly)
                                                select Path.GetFileName(fullFilename);

            return string.IsNullOrWhiteSpace(filePattern) ? excelFiles : (from file in excelFiles where file.StartsWith(filePattern) select file);
        }
    }
}
