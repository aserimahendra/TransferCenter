namespace TransferCenterDbStore.Entities;

public abstract class AuditLogMeta
{
    public string CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public DateTime? LastUpdatedOn { get; set; }
}
