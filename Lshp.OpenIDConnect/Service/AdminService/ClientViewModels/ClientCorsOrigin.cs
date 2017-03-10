using System.ComponentModel.DataAnnotations;

namespace Lshp.OpenIDConnect.Service.AdminService.ClientViewModels
{
    public class ClientCorsOrigin
    {
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public string Origin { get; set; }
    }
}
