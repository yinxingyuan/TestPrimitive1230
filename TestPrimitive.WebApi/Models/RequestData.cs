using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MetaShare.Common.Core.Entities;
using Newtonsoft.Json;
namespace TestPrimitive.WebApi.Models
{
    public class RequestData
    {
        public string PagerJsonString { get; set; }
        public string EntityJsonString { get; set; }
        public string ColumnNamesJsonString { get; set; }
    }

    [DataContract]
    public class RequestData<T>
    {
        [DataMember]
        public Pager Pager { get; set; }

        [DataMember]
        public T Entity { get; set; }

        [DataMember]
        public List<string> Columns { get; set; }

        public void Deserialize(RequestData requestData)
        {
            if (requestData == null) throw new ArgumentNullException("requestData");

            Pager = JsonConvert.DeserializeObject<Pager>(requestData.PagerJsonString);
            Entity = JsonConvert.DeserializeObject<T>(requestData.EntityJsonString);
            Columns = JsonConvert.DeserializeObject<List<string>>(requestData.ColumnNamesJsonString);
        }

        public Dictionary<string, string> Serialize()
        {
            string pagerJsonString = JsonConvert.SerializeObject(this.Pager);
            string entityJsonString = JsonConvert.SerializeObject(this.Entity);
            string propertyNamesJsonString = JsonConvert.SerializeObject(this.Columns);

            Dictionary<string, string> requestDataDictionary = new Dictionary<string, string>();
            requestDataDictionary.Add("PagerJsonString", pagerJsonString);
            requestDataDictionary.Add("EntityJsonString", entityJsonString);
            requestDataDictionary.Add("ColumnNamesJsonString", propertyNamesJsonString);
            return requestDataDictionary;
        }
    }
}