using System;
using System.ComponentModel.DataAnnotations;

namespace TransferCenterWeb.Models;

public class AuditLog
{
    public long Id { get; set; }                      // BIGINT IDENTITY
    public string UserId { get; set; }                   // User performing the action
    public required string ControllerName { get; set; }        // Controller or Module name
    public required string ActionName { get; set; }            // Specific method or API endpoint
    public string? ActionType { get; set; }           // Optional: Create / Update / Delete / View
    public string? RequestData { get; set; }          // Input data (as JSON)
    public string? ResponseData { get; set; }         // Output or response data
    public string? ExecutionStatus { get; set; }      // Success / Failed / Exception
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime ExecutionDate { get; set; }       // UTC timestamp
    public string? ClientIp { get; set; }             // IP address of request
    public string? Remarks { get; set; }              // Additional info or error details
}

public abstract class AuditLogMeta
{
    public string? CreatedBy { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime CreatedOn { get; set; } = DateTime.Now;

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? LastUpdatedOn { get; set; }
}