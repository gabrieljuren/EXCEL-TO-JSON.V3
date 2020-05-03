using ExcelDataReader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Relatorio_api.Models;

namespace Relatorio_api.Repositorio
{
    public class ReturnJsString
    {
        public string returnData(PathFile pathFile)
        {
            DataTable dt = new DataTable();

            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;

            FileInfo fileInfo = new FileInfo(pathFile.path);

            using (var stream = File.Open(pathFile.path, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader reader;
                reader = ExcelReaderFactory.CreateReader(stream);

                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };

                var dataSet = reader.AsDataSet(conf);

                dt = dataSet.Tables[0];
            }

            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            string result = JsonConvert.SerializeObject(parentRow);

            if (fileInfo.Exists)
                fileInfo.Delete();

            return result;
        }
    }
}