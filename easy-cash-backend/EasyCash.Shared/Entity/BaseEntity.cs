using System.ComponentModel.DataAnnotations;

namespace EasyCash.Shared.Entity;

public abstract class BaseEntity<TId> : BaseEntityTimeStamp
{
    [Key]
    public TId Id { get; set; }
    public bool IsDeleted { get; set; } = false;

}

public abstract class BaseEntityTimeStamp
{
    public BaseEntityTimeStamp()
    {
        CreatedOn = DateTime.UtcNow;
    }

    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
}