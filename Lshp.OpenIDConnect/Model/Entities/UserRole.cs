namespace Lshp.OpenIDConnect.Model.Entities
{
    public class UserRole : BaseEntity
    {
        public virtual long UserId { get; set; }
        public virtual long RoleId { get; set; }
    }
}
