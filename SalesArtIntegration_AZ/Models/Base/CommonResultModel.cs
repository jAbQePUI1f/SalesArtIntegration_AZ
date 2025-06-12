using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesArtIntegration_AZ.Models.Base
{
    public class CommonResultModel
    {
        private List<string> messsages;
        public bool State { get; set; }
        public List<string> Messages
        {
            get
            {
                if (messsages == null)
                    messsages = new List<string>();
                return messsages;
            }
            set
            {
                messsages = value;
            }
        }
    }
}
