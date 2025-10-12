namespace TransferCenterDbStore.Entity;

public class AdditionalInfo
{
    public int Id { get; set; }

    public string KeyName { get; set; }

    public string Type { get; set; }

    public string KeyValue { get; set; }

    public string Status { get; set; } 

    public bool IsDeleted { get; set; }

    public string ModuleName { get; set; }

    public int ModuleId { get; set; }
}
