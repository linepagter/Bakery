using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Bakery.Models;

public class BakeryUser : IdentityUser
{
    [MaxLength(100)]
    public string? UserName { get; set; }
    
    public string Role { get; set; }
    
}