﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Model.Entities
{
    public class ClientGrantType
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string GrantType { get; set; }
    }
}
