using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Login_Accounts.Classes;

public class AuthOptions
{
    public const string ISSUER = "ServiseAuto27";
    public const string AUDIENCE = "Backend12"; 
    const string KEY = "SymphonyNo5inCMinorOp67DestinyBeethoven"; 
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}
