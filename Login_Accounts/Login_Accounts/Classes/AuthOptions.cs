using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Login_Accounts.Classes;

public class AuthOptions
{
    public const string ISSUER = "ServiseAuto27"; // издатель токена
    public const string AUDIENCE = "Backend12"; // потребитель токена
    const string KEY = "MissPenelopeEchart1987ThisMyLove";   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}
