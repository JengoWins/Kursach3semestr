using Microsoft.AspNetCore.Mvc;

namespace EnergencyService.Models.Template;

public class AccountTemplate
{
    [FromQuery]
    public string Email { get; set; }
    [FromQuery]
    public string password { get; set; }
}
