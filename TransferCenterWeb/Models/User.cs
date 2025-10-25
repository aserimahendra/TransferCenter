using System;

namespace TransferCenterWeb.Models;

public class User
{
    public long UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string EmailId { get; set; } = string.Empty;
    public string? Password { get; set; }
    public string DomainID { get; set; } = string.Empty;    
    public string LoginId { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public short Role { get; set; }
    public bool IsActive { get; set; } = true;
}