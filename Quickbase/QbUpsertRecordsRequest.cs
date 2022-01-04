using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Quickbase
{
    public class QbUpsertRecordsRequest<T> where T : IQbRecord
    {
        private IQbContext _QbContext;
        private string _to;
        private QbDataset<T> _data;
        private int _mergeFieldId;
        private List<int> _fieldsToReturn;

        public QbUpsertRecordsRequest(IQbContext context, QbDataset<T> dataset)
        {
            _QbContext = context;
            _data = dataset;
            SetTo(dataset);
        }
        public QbUpsertRecordsRequest(IQbContext context, QbDataset<T> dataset, List<int> fieldsToReturn)
        {
            _QbContext = context;
            _data=dataset;
            _fieldsToReturn = fieldsToReturn;
            SetTo(dataset);
        }
        public QbUpsertRecordsRequest(IQbContext context, QbDataset<T> dataset, int mergeFieldId)
        {
            _QbContext = context;
            _data =dataset;
            _mergeFieldId = mergeFieldId;
            SetTo(dataset);
        }
        public QbUpsertRecordsRequest(IQbContext context, QbDataset<T> dataset, List<int> fieldsToReturn, int mergeFieldId)
        {
            _QbContext = context;
            _data = dataset;
            _fieldsToReturn = fieldsToReturn.ToList();
            _mergeFieldId=mergeFieldId;
            SetTo(dataset);
        }

        public void Send()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.quickbase.com/v1/");


                List<Dictionary<string, object>> recordsWithoutUnwritableFields = new();

                recordsWithoutUnwritableFields = RemoveUnwritableFields(recordsWithoutUnwritableFields);

                var postBody = new QbPostRecordsBody()
                {
                    to = _to,
                    data = recordsWithoutUnwritableFields,
                    fieldsToReturn = _fieldsToReturn,
                    mergeFieldId = _mergeFieldId
                };

                var json = JsonSerializer.Serialize(postBody);
                Console.WriteLine(json);

                client.DefaultRequestHeaders.Add("QB-Realm-Hostname", _QbContext.Realm);
                client.DefaultRequestHeaders.Add("Authorization", $"QB-USER-TOKEN {_QbContext.Usertoken}");

                var response = client.PostAsJsonAsync("records", postBody).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.Write("Success");
                }
                else
                    Console.Write("Error");
            }
        }

        private List<Dictionary<string, object>> RemoveUnwritableFields(List<Dictionary<string, object>> recordsWithoutUnwritableFields)
        {
            var records = _data.GetRecordsAsDictionary();

            foreach (var rec in records)
            {
                var record = new Dictionary<string, object>();
                foreach (var field in rec)
                {
                    if (field.Key != "1" && field.Key != "2" && field.Key != "4" && field.Key != "5")
                    {
                        record.Add(field.Key, field.Value);
                    }
                }

                recordsWithoutUnwritableFields.Add(record);
            }

            return recordsWithoutUnwritableFields;
        }

        private void SetTo(QbDataset<T> dataset)
        {
            DbidAttribute dbidAttribute = (DbidAttribute)dataset.GetType().GetCustomAttribute(typeof(DbidAttribute));
            _to = dbidAttribute.Dbid.ToString();
        }
    }
}
