using log4net;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
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
                positionLimitFileParser.FileParser(Config.Default.SourceFolder, files);

            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
            
        }

        public void FileParser(string folder, IEnumerable<string> files)
        {
            Logger.Info(LogHelper.LogInfo(MethodBase.GetCurrentMethod(), folder, files.Count()));
            
            Excel.Application excel = new Excel.Application();
            excel.DisplayAlerts = false;

            foreach (string file in files)
            {
                string fullyQualifiedFileName = Path.Combine(folder, file);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                string csvFileName = String.Format($"{fileNameWithoutExtension}.csv");
                string fullyQualifiedCsvFileName = Path.Combine(folder, csvFileName);

                Logger.Info($"{fullyQualifiedFileName} to {fullyQualifiedCsvFileName}");
                
                Excel.Workbook xlBook = excel.Workbooks.Open(fullyQualifiedFileName);
                //Excel.Worksheet xlSheet = (Excel.Worksheet)xlBook.Worksheets["Sheet1"];
                Excel.Worksheet xlSheet = (Excel.Worksheet)xlBook.Worksheets[1];
                xlSheet.Select(Type.Missing);
                xlBook.SaveAs(fullyQualifiedCsvFileName, Excel.XlFileFormat.xlCSV, Excel.XlSaveAsAccessMode.xlNoChange);
                xlBook.Close(SaveChanges:false);
                
            }

            excel.Quit();
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
