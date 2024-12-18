using Microsoft.AspNetCore.Mvc;
using DataBasePostgres.Models.AutoReg;
using DataBasePostgres.DataBaseClasses;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Login_Accounts.Classes;
using Newtonsoft.Json;

namespace Login_Accounts.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RegAutorizController : ControllerBase
{
    private ApplicationContext context;
    private RegistrationModel? RegModel;
    private AutorizationModel? AutoModel;

    public RegAutorizController(ApplicationContext context) {
        this.context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetReLoadController()
    {
        return Ok("So, I'm job in Controller");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAccount()
    {
        try
        {
            return Ok(context.Account.ToList());
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public IActionResult GetTokenAccount(string email, string password)
    {
        try
        {
            // находим пользователя 
            AutoModel = context.Account.FirstOrDefault(a => a.email == email && a.password == password);
            // если пользователь не найден, отправляем статусный код 404
            if (AutoModel is null)// || AutoModel.Role != "admin")
                return BadRequest("Ну типо, 404");

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, AutoModel.email) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,"Cookies");
            var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(20)),  // действие токена истекает через 2 минуты
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // формируем ответ
            var response = new
            {
                access_token = encodedJwt,
                username = AutoModel.email
            };

            return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
        }
        catch (Exception ex)
        {
            var json = JsonConvert.SerializeObject(ex)!;
            return BadRequest(json);
        }
    }

    [HttpPost]
    public async Task<IActionResult> RegistrationAccount(FullModelAccount modelAccount)
    {
        try
        {
            RegModel = new RegistrationModel
            {
                username = modelAccount.UserName,
                dateofbirths = modelAccount.Dateofbirths
            };
            context.Account_Info.AddRange(RegModel);
            await context.SaveChangesAsync();
            AutoModel = new AutorizationModel
            {
                id_info = RegModel.id,
                email = modelAccount.Email,
                password = modelAccount.password
            };
            context.Account.AddRange(AutoModel);
            await context.SaveChangesAsync();
            return Ok(AutoModel);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
