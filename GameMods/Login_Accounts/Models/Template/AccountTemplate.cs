using Microsoft.AspNetCore.Mvc;

namespace Login_Accounts.Models.Template;

public class AccountTemplate
{
    [FromQuery]
    public string Email { get; set; }
    [FromQuery]
    public string password { get; set; }
}

public class ProfileTemplate
{
    public string username { get; set; }
    public string email { get; set; }
    public DateTime date { get; set; }
}
