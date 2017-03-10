using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Model.Entities
{
    public class ClientScope
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Scope { get; set; }
    }
}
