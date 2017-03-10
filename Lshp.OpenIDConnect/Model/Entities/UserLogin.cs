namespace Lshp.OpenIDConnect.Model.Entities
{
    public class UserLogin : BaseEntity
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public long UserId { get; set; }
    }
}
