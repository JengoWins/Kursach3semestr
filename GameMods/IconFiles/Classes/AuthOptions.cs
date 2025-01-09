using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IconFiles.Classes;

public class AuthOptions
{
    public const string ISSUER = "ServiseAuto27"; // издатель токена
    public const string AUDIENCE = "Backend12"; // потребитель токена
    const string KEY = "SymphonyNo5inCMinorOp67DestinyBeethoven";   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}
