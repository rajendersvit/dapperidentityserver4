using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Model.Entities
{
    public class ApiResource
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public Boolean Enabled { get; set; }
        public string Name { get; set; }
        public List<ApiSecret> ApiSecrets { get; set; }
        public List<ApiScope> ApiScopes { get; set; }
        public List<ApiScopeClaim> ApiScopeClaim { get; set; }
    }
}
