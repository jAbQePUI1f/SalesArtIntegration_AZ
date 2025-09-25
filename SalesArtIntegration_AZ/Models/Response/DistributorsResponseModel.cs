using Newtonsoft.Json;
using SalesArtIntegration_AZ.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesArtIntegration_AZ.Models.Response
{
    internal class DistributorsResponseModel: BaseResponseJson
    {
       public   Distributors data { get; set; }

        // Data sınıfı
        public class Distributors
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("code")]
            public string Code { get; set; }
        }
    }
}
