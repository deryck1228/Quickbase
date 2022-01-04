namespace Quickbase
{
    [Dbid("")]
    public abstract class QbRecord : IQbRecord
    {
        private string _dbid { get; set; }

        [FieldId("1", "QbTextField")]
        public string DateCreated { get; init; }
        [FieldId("3", "QbTextField")]
        public string RecordID { get; init; }


        public Dictionary<string, object> GetFieldData()
        {
            Dictionary<string, object> data = new();

            var props = this.GetType().GetProperties().Where(
               prop => Attribute.IsDefined(prop, typeof(FieldIdAttribute)));

            foreach(var prop in props)
            {
                FieldIdAttribute fieldIdAttribute = (FieldIdAttribute)Attribute.GetCustomAttribute(prop, typeof(FieldIdAttribute));
                var propValue = prop.GetValue(this);
                data.Add(fieldIdAttribute.Fid, new { value = propValue });
            }

            return data;
        }

        public void SetFieldData(Dictionary<string, object> data)
        {
            var props = this.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(FieldIdAttribute)));

            foreach (var prop in props)
            {
                FieldIdAttribute fieldIdAttribute = (FieldIdAttribute)Attribute.GetCustomAttribute(prop, typeof(FieldIdAttribute));
                var propValue = prop.GetValue(this);

                if (data.ContainsKey(fieldIdAttribute.Fid))
                {
                    var thisDictEntry = data.Where(e => e.Key == fieldIdAttribute.Fid);

                    if (thisDictEntry is not null)
                    {
                        var valueToSet = data[fieldIdAttribute.Fid].ToString();
                        var valueKind = valueToSet.GetType();
                        prop.SetValue(this, valueToSet);
                    } 
                }
            }
        }

        public void MapPropertiesToFids(Dictionary<string,string> propertyToFidMapping)
        {
            var props = this.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(FieldIdAttribute)));

            foreach(var prop in props)
            {
                FieldIdAttribute fieldIdAttribute = (FieldIdAttribute)Attribute.GetCustomAttribute(prop, typeof(FieldIdAttribute));
                if (propertyToFidMapping.ContainsKey(prop.Name)) {
                    {
                        var fid = propertyToFidMapping[prop.Name];
                    
                    if (fid is not null)
              
                           fieldIdAttribute.Fid = fid;
                    }
                }
            }
        }
    }
}
