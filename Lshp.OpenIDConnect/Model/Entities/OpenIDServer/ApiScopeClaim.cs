using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Model.Entities
{
    public class ApiScopeClaim
    {
        public int Id { get; set; }
        public int ApiScopeId { get; set; }
        public string Type { get; set; }
    }
}
