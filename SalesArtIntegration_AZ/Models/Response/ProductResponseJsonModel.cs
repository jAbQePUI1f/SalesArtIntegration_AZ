using Newtonsoft.Json;
using SalesArtIntegration_AZ.Models.Base;

namespace SalesArtIntegration_AZ.Models.Response
{
    public class ProductResponseJsonModel : BaseResponseJson
    {
        public DataContainer? data { get; set; }

        public class DataContainer
        {
            public List<Products>? products { get; set; }
        }
        public class Products
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("code")]
            public string? Code { get; set; }

            [JsonProperty("mainUnitName")]
            public string? UnitName { get; set; }
            [JsonProperty("activeUnits")]
            public List<ActiveUnit>? ActiveUnits { get; set; }
        }
        public class ActiveUnit
        {
            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("code")]
            public string? Code { get; set; }

            [JsonProperty("conversionFactor")]
            public double ConversionFactor { get; set; }

            [JsonProperty("area")]
            public double Area { get; set; }

            [JsonProperty("grossVolume")]
            public double GrossVolume { get; set; }

            [JsonProperty("grossWeight")]
            public double GrossWeight { get; set; }

            [JsonProperty("height")]
            public double Height { get; set; }

            [JsonProperty("length")]
            public double Length { get; set; }

            [JsonProperty("volume")]
            public double Volume { get; set; }

            [JsonProperty("weight")]
            public double Weight { get; set; }

            [JsonProperty("width")]
            public double Width { get; set; }

            [JsonProperty("mainUnit")]
            public bool MainUnit { get; set; }

        }
    }
}
