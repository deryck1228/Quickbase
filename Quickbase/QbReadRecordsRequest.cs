using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Quickbase
{
    public class QbReadRecordsRequest<T> where T : IQbRecord, new()
    {
        private IQbContext _QbContext;
        private string _from;
        private QbDataset<T> _data;
        private string _where;
        private List<object> _select;

        public QbReadRecordsRequest(IQbContext context, QbDataset<T> dataset)
        {
            _QbContext = context;
            _data = dataset;
            SetFrom(dataset);
        }

        public QbReadRecordsRequest(IQbContext context, QbDataset<T> dataset, string where)
        {
            _QbContext = context;
            _data = dataset;
            _where = where;
            SetFrom(dataset);
        }

        public QbReadRecordsRequest(IQbContext context, QbDataset<T> dataset, List<object> select)
        {
            _QbContext = context;
            _data = dataset;
            _select = select;
            SetFrom(dataset);
        }

        public QbReadRecordsRequest(IQbContext context, QbDataset<T> dataset, string where, List<object> select)
        {
            _QbContext = context;
            _data = dataset;
            _where = where;
            _select = select;
            SetFrom(dataset);
        }

        public async Task<IQbDataset<T>> Read()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.quickbase.com/v1/");

                var postBody = new QbGetRecordsBody()
                {
                    from = _from,
                    select = _select,
                    where = _where

                };

                var json = JsonSerializer.Serialize(postBody);
                Console.WriteLine(json);

                client.DefaultRequestHeaders.Add("QB-Realm-Hostname", _QbContext.Realm);
                client.DefaultRequestHeaders.Add("Authorization", $"QB-USER-TOKEN {_QbContext.Usertoken}");

                var response = client.PostAsJsonAsync("records/query", postBody).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.Write("Success");

                    var parsedData = await JsonDocument.ParseAsync(response.Content.ReadAsStream());

                    return ConvertResponseDataToQbDataset(parsedData);
                }
                else
                {
                    Console.Write("Error");
                    return null;
                }
                
            }

        }

        private IQbDataset<T> ConvertResponseDataToQbDataset(JsonDocument? parsedData)
        {
            var dataType = _data.GetType();
            var newDataInstance = Activator.CreateInstance(dataType) as IQbDataset<T>;

            var dataNodes = parsedData.RootElement.GetProperty("data").EnumerateArray();

            foreach (var record in dataNodes)
            {
                var newQbRecord = new T() as QbRecord;

                foreach (var field in record.EnumerateObject())
                {
                    var rid = field.Name;
                    var valueNode = field.Value;
                    var value = valueNode.GetProperty("value");

                    Dictionary<string, object> values = new Dictionary<string, object>();
                    values.Add(rid, value);
                    newQbRecord.SetFieldData(values);
                }

                newDataInstance.AddRecord(newQbRecord);
            }


            return newDataInstance;
        }

        private void SetFrom(QbDataset<T> dataset)
        {
            DbidAttribute dbidAttribute = (DbidAttribute)dataset.GetType().GetCustomAttribute(typeof(DbidAttribute));
            _from = dbidAttribute.Dbid.ToString();
        }
    }
}
