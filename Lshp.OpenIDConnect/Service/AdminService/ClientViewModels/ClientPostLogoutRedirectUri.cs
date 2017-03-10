using System.ComponentModel.DataAnnotations;

namespace Lshp.OpenIDConnect.Service.AdminService.ClientViewModels
{
    public class ClientPostLogoutRedirectUri
    {
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public string PostLogoutRedirectUri { get; set; }
    }
}
