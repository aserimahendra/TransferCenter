using System;
using System.ComponentModel.DataAnnotations;

namespace TransferCenterWeb.Models;

public class User
{
    public long UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Please enter a valid email address.")]
    public string EmailId { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$", ErrorMessage = "Password must be at least 8 characters and include upper, lower, number, and special character.")]
    public string? Password { get; set; }
    public string DomainID { get; set; } = string.Empty;

    [Required]
    [StringLength(100, ErrorMessage = "Login ID must be less than 100 characters.")]
    public string LoginId { get; set; } = string.Empty;
    
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public short Role { get; set; }
    public bool IsActive { get; set; } = true;
    public string? CreatedBy { get; set; } 
    
}