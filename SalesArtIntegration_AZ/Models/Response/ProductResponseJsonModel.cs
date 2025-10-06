using Newtonsoft.Json;
using SalesArtIntegration_AZ.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesArtIntegration_AZ.Models.Response
{
    public class ProductResponseJsonModel : BaseResponseJson
    {
        public DataContainer data { get; set; }

        public class DataContainer
        {
            public List<Products> products { get; set; }
        }
        public class Products
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("code")]
            public string Code { get; set; }

            [JsonProperty("mainUnitName")]
            public string UnitName { get; set; }
        }
    }
}
