using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesArtIntegration_AZ.Models.Request
{
    public class CollectionSyncRequest
    {
        public IntegratedCollection[] integratedCollections { get; set; }
        public class IntegratedCollection
        {
            public bool synced { get; set; }
            public string ficheNo { get; set; }
            public string remoteCollectionNumber { get; set; }
            public string message { get; set; }
        }
    }
}
