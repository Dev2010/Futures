using log4net;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace CMEGroupPositionLimitFileParser
{
    public class CMEPositionLimitFileParser
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public string Folder { get; }
        public string FileName { get; }
        public string FullyQualifiedFileName { get; }

        Dictionary<string, string> CMEHeaderValueToColumns = new Dictionary<string, string>();
        public CMEPositionLimitFileParser(string folder, string fileName)
        {
            Folder = folder;
            FileName = fileName;
            FullyQualifiedFileName = Path.Combine(folder, fileName);
        }

        private void Init()
        {
            var text = File.ReadAllText(Config.Default.CMEHeaderMapFile);
            CMEHeaderValueToColumns = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);
        }
        public DataTable Read()
        {
            Init();


            if (! File.Exists(FullyQualifiedFileName))
            {
                throw new FileNotFoundException();
            }

            return ExcelHelper.GetDataSetFromExcelSheet(FullyQualifiedFileName, 1, Config.Default.CMEPositionLimitFileHeaderMarker, Config.Default.CMEPositionLimitFileFooterMarker, CMEHeaderValueToColumns);
        }
    }
}
