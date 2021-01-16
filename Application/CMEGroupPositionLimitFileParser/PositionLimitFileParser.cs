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
            positionLimitFileParser.Process();
        }

        public void Process()
        {
            PositionLimitFileParser positionLimitFileParser = new PositionLimitFileParser();

            try
            {
                IEnumerable<string> files = positionLimitFileParser.SearchFiles(Config.Default.SourceFolder, string.Empty);
                SqlHelper sqlHelper = new SqlHelper(Config.Default.ExchangeDatabaseConnectionString);
                //ExcelHelper.ConvertExcelSheetToCsv(Config.Default.SourceFolder, files);
                foreach (string fileName in files)
                {
                    if (fileName.StartsWith(Config.Default.CMEPositionLimitFilePattern, true, CultureInfo.InvariantCulture))
                    {
                        CMEPositionLimitFileParser cmePositionLimitFileParser = new CMEPositionLimitFileParser(Config.Default.SourceFolder, fileName);
                        DataTable dtCME = cmePositionLimitFileParser.Read();

                        // add run_id
                        System.Data.DataColumn run_id = new System.Data.DataColumn("run_id", typeof(long));
                        run_id.DefaultValue = sqlHelper.GetSequenceNextVal(Config.Default.spNameGetNextSequenceIncrementBy1);
                        dtCME.Columns.Add(run_id);
                        // add create_date
                        System.Data.DataColumn create_date = new System.Data.DataColumn("create_date", typeof(DateTime));
                        create_date.DefaultValue = DateTime.Now;
                        dtCME.Columns.Add(create_date);
                        //create_user
                        System.Data.DataColumn create_user = new System.Data.DataColumn("create_user", typeof(string));
                        create_user.DefaultValue = Config.Default.DBUser;
                        dtCME.Columns.Add(create_user);
                        //last_update_date,
                        System.Data.DataColumn last_update_date = new System.Data.DataColumn("last_update_date", typeof(DateTime));
                        last_update_date.DefaultValue = DateTime.Now;
                        dtCME.Columns.Add(last_update_date);
                        //last_update_user,
                        System.Data.DataColumn last_update_user = new System.Data.DataColumn("last_update_user", typeof(string));
                        last_update_user.DefaultValue = Config.Default.DBUser;
                        dtCME.Columns.Add(last_update_user);
                        
                        sqlHelper.Save(Config.Default.spNameSaveCMEFuturePositionLimit, dtCME);
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
