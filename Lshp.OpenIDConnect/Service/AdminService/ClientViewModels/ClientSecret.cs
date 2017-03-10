using System;
using System.ComponentModel.DataAnnotations;

namespace Lshp.OpenIDConnect.Service.AdminService.ClientViewModels
{
    public class ClientSecret
    {
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        public string Description { get; set; }
        public DateTime? Expiration { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
