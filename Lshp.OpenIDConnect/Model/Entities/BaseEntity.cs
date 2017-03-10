using System;

namespace Lshp.OpenIDConnect.Model.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
        }
        public long Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }        
        public DateTime? DeletedAt { get; set; }
    }
}
