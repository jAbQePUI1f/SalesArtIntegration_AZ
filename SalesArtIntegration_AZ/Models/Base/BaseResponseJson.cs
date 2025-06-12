using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesArtIntegration_AZ.Models.Base
{
    public class BaseResponseJson
    {
        public Message message { get; set; }
        public int responseStatus { get; set; }
        public class Message
        {
            public string code { get; set; }
            public string defaultMessage { get; set; }
        }
    }
}
