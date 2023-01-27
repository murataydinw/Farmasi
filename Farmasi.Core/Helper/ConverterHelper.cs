using System.Data;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using Nancy.Json;
using Newtonsoft.Json;

namespace Farmasi.Core.Helper
{
    public static class ConverterHelper
    {
        public static byte[] ModelToByteArray<T>(T model)
        {
            if (model == null)
            {
                return null;
            }

            var jsonModel = JsonConvert.SerializeObject(model);
            var byteArray = Encoding.UTF8.GetBytes(jsonModel);

            return byteArray;
        }

        public static T ByteArrayToModel<T>(byte[] byteArray)
        {
            if (byteArray == null)
            {
                return default;
            }

            var jsonModel = Encoding.UTF8.GetString(byteArray);
            var model = JsonConvert.DeserializeObject<T>(jsonModel);

            return model;
        }
         public static T JsonToModel<T>(this string jsonResult) where T : class, new()
        {
            if (jsonResult == null)
            {
                return new T();
            }
            if (string.IsNullOrEmpty(jsonResult.Trim())) return new T();
            var js = new JavaScriptSerializer();
            var model = js.Deserialize<T>(jsonResult);
            return model;
        }

        public static string ModelToJson<T>(this T obj) where T : class, new()
        {
            try
            {
                if (obj == null) return string.Empty; 
                var js = new JavaScriptSerializer();
                var json = js.Serialize(obj);
                return json;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static T XmlToModel<T>(this string xmlResult) where T : class, new()
        {
            if (string.IsNullOrEmpty(xmlResult.Trim())) return new T();
            var xmlSerializer = new XmlSerializer(typeof(T));
            var stringReader = new StringReader(xmlResult);
            var model = (T)xmlSerializer.Deserialize(stringReader);
            stringReader.Dispose();
            return model;
        }

        public static string ModelToXml<T>(this T obj) where T : class, new()
        {
            if (obj == null) return string.Empty;
            var xmlSerializer = new XmlSerializer(obj.GetType());
            var stringWriter = new StringWriter();
            xmlSerializer.Serialize(stringWriter, obj);
            var xmlOutput = stringWriter.ToString();
            stringWriter.Dispose();
            return xmlOutput;
        }

        public static T DataTableToModel<T>(this DataTable dataTable) where T : class, new()
        {
            var model = new T();

            if (dataTable == null) return null;
            var row = dataTable.Rows[0];
            foreach (var prop in model.GetType().GetProperties())
            {
                var propertyInfo = model.GetType().GetProperty(prop.Name);
                propertyInfo?.SetValue(model, Convert.ChangeType((row[prop.Name] == DBNull.Value) ? string.Empty : row[prop.Name], propertyInfo.PropertyType), null);
            }
            return model;
        }

        public static DataTable ModelToDataTable<T>(this T obj) where T : class, new()
        {
            var dataTable = new DataTable();
            var propertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in propertyInfo)
            {
                dataTable.Columns.Add(prop.Name);
            }

            var values = new object[propertyInfo.Length];
            for (var i = 0; i < propertyInfo.Length; i++)
            {
                values[i] = propertyInfo[i].GetValue(obj, null);
            }

            dataTable.Rows.Add(values);
            return dataTable;
        }

        public static List<T> DataTableToModelList<T>(this DataTable dataTable) where T : class, new()
        {
            if (dataTable == null) return null;
            var modelList = new List<T>();
            foreach (var row in dataTable.AsEnumerable())
            {
                var model = new T();
                foreach (var prop in model.GetType().GetProperties())
                {
                    var propertyInfo = model.GetType().GetProperty(prop.Name);
                    propertyInfo?.SetValue(model, Convert.ChangeType((row[prop.Name] == DBNull.Value) ? string.Empty : row[prop.Name], propertyInfo.PropertyType), null);
                }
                modelList.Add(model);
            }
            return modelList;
        }

        public static DataTable ModelListToDataTable<T>(this List<T> obj) where T : class, new()
        {
            var dataTable = new DataTable();
            var propertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in propertyInfo)
            {
                dataTable.Columns.Add(prop.Name);
            }

            foreach (var model in obj)
            {
                var values = new object[propertyInfo.Length];
                for (var i = 0; i < propertyInfo.Length; i++)
                {
                    values[i] = propertyInfo[i].GetValue(model, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static T DataRowToModel<T>(this DataRow row) where T : class, new()
        {
            if (row == null) return null;
            var model = new T();
            var properties = model.GetType().GetProperties();
            foreach (var property in properties)
            {
                property.SetValue(model, (row[property.Name] == DBNull.Value) ? null : row[property.Name]);
            }
            return model;
        }

        public static DataRow ModelToDataRow<T>(this T obj) where T : class, new()
        {
            var dataRow = ModelToDataTable(obj).Rows[0];
            return dataRow;
        }

        public static string DataTableToJson(DataTable dataTable)
        {
            if (dataTable.Rows.Count < 1)
            {
                return string.Empty;
            }
            var js = new JavaScriptSerializer();
            var rows = (from DataRow dr in dataTable.Rows select dataTable.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col])).ToList();
            return js.Serialize(rows);
        }
    }
}
