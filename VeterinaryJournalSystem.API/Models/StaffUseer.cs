
using Microsoft.AspNetCore.Identity;

namespace VeterinaryJournalSystem.Models;

public class StaffUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    public string StaffCode { get; set; } = string.Empty;
}