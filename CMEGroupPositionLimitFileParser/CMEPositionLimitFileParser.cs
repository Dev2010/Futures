using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace CMEGroupPositionLimitFileParser
{
    public class CMEPositionLimitFileParser
    {
        public string Folder { get; }
        public string FileName { get; }
        public string FullyQualifiedFileName { get; }
        public CMEPositionLimitFileParser(string folder, string fileName)
        {
            Folder = folder;
            FileName = fileName;
            FullyQualifiedFileName = Path.Combine(folder, fileName);
        }

        public DataSet Read()
        {
            DataSet ds = null;

            if (! File.Exists(FullyQualifiedFileName))
            {
                throw new FileNotFoundException();
            }

            ExcelHelper.GetDataSetFromExcelSheet(FullyQualifiedFileName, 1, Config.Default.CMEPositionLimitFileHeaderMarker, Config.Default.CMEPositionLimitFileFooterMarker);

            return null;
        }
    }
}
