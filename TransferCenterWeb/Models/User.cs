using System;
using System.ComponentModel.DataAnnotations;

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
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime CreatedOn { get; set; }
    public short Role { get; set; }
    public bool IsActive { get; set; } = true;
}