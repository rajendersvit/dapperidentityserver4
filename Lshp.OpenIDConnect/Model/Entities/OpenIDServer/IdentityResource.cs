using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Model.Entities
{
    public class IdentityResource
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public Boolean Emphasize { get; set; }
        public Boolean Enabled { get; set; }
        public string Name { get; set; }
        public Boolean Required { get; set; }
        public Boolean ShowInDiscoveryDocument { get; set; }
        public List<IdentityClaim> IdentityClaims { get; set; }
    }
}
