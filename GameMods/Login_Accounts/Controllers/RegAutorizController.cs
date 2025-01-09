using Microsoft.AspNetCore.Mvc;
using Login_Account.Models.AutoReg;
using Login_Account.DataBaseClasses;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Login_Accounts.Classes;
using Newtonsoft.Json;
using Login_Accounts.RabbitMQ;
using Login_Accounts.Models.Template;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;

namespace Login_Accounts.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RegAutorizController : ControllerBase
{
    private ApplicationContext context;
    private readonly ILogger logger;

    public RegAutorizController(ApplicationContext context, ILogger<RegAutorizController> logger)
    {
        this.context = context;
        this.logger = logger;
    }
    /// <summary>
    /// Тестировачный поинт
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetTestController()
    {
        return Ok("So, I'm job in Controller. ТЕСТИРОВАНИЕ ЗАВЕРШЕНО");
    }
    /// <summary>
    /// Запрос на проверку пользователя
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetCheckAccount(string username)
    {
        try
        {
            bool isCheck = false;
            RegistrationModel? Account_In = context.Account_Info.FirstOrDefault(a => a.username == username);
            if (Account_In is null)
            {
                logger.LogWarning($"Модель была пустой (RegistrationModel):" + "Time: {DateTime.Now.ToLongTimeString()}");
                return BadRequest("Не найдены записи об аккаунте");
            }
            UserModel? users = context.User.FirstOrDefault(a => a.id_info == Account_In.id);
            if (users is null)
            {
                logger.LogWarning($"Модель была пустой (UserModel):" + "Time: {DateTime.Now.ToLongTimeString()}");
                return BadRequest("Не найден ключ");
            }
            else
            {
                isCheck = true;
            }
            return Ok(isCheck);
        }
        catch (Exception ex)
        {
            return BadRequest("Выпало исключение: " + ex);
        }
    }
    /// <summary>
    /// Запрос на вывод ключа пользователя для создание поста
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> PostCreateUser(string username)
    {
        try
        {
            RegistrationModel? Account_In = context.Account_Info.FirstOrDefault(a => a.username == username);
            if (Account_In is null)
            {
                logger.LogWarning($"Модель была пустой (RegistrationModel):" + "Time: {DateTime.Now.ToLongTimeString()}");
                return BadRequest("Не найдены записи об аккаунте");
            }
            UserModel? users = context.User.FirstOrDefault(a => a.id_info == Account_In.id);
            if (users is null)
            {
                logger.LogWarning($"Модель была пустой (UserModel):" + "Time: {DateTime.Now.ToLongTimeString()}");
                return BadRequest("Не найден ключ");
            }

            RabbitMq rabbitMq = new RabbitMq();
            rabbitMq.SendMessage(users.id, "PostCreateUser");
            return Ok("Отправка в Реббит...");
        }
        catch (Exception ex)
        {
            return BadRequest("Выпало исключение: " + ex);
        }
    }
    /// <summary>
    /// Запрос на вывод данных пользователя, исходя из данных поста
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> PostGetUser()
    {
        try
        {
            RabbitMq rabbit = new RabbitMq();
            List<Guid> listGuid = await rabbit.ReceiveMessage("PostGetUser");
            string listString = "";
            foreach (Guid idUser in listGuid)
            {
                UserModel? users = context.User.FirstOrDefault(a => a.id == idUser);
                if (users is null)
                {
                    logger.LogWarning($"Модель была пустой (UserModel):" + "Time: {DateTime.Now.ToLongTimeString()}");
                    return BadRequest("Не найден ключ");
                }

                RegistrationModel? Account_In = context.Account_Info.FirstOrDefault(a => a.id == users.id_info);
                if (Account_In is null)
                {
                    logger.LogWarning($"Модель была пустой (RegistrationModel):" + "Time: {DateTime.Now.ToLongTimeString()}");
                    return BadRequest("Не найдены записи об аккаунте");
                }

                listString = Account_In.username;
            }
           
            return Ok(listString);
        }
        catch (Exception ex)
        {
            return BadRequest("Выпало исключение: " + ex);
        }
    }
    /// <summary>
    /// Запрос на вывод данных аккаунта по имени
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAccount(string username)
    {
        try
        {
            RegistrationModel? Account_In = context.Account_Info.FirstOrDefault(a => a.username == username);
            if (Account_In is null)
            {
                logger.LogWarning($"Модель была пустой (RegistrationModel):" + "Time: " + DateTime.Now.ToLongTimeString());
                return BadRequest("Не найдены записи об аккаунте");
            }
            UserModel? users = context.User.FirstOrDefault(a => a.id_info == Account_In.id);
            if (users is null)
            {
                logger.LogWarning($"Модель была пустой (UserModel):" + "Time: "+DateTime.Now.ToLongTimeString());
                return BadRequest("Не найден ключ");
            }
            AutorizationModel? AutoModel = context.Account.FirstOrDefault(a => a.id == users.id_account);
            if (AutoModel is null)
            {
                logger.LogWarning($"Модель была пустой (AutorizationModel):" + "Time: "+DateTime.Now.ToLongTimeString());
                return BadRequest("Не найден аккаунт");
            }
            ProfileTemplate profile = new ProfileTemplate
            {
                username = Account_In.username,
                email = AutoModel.email,
                date = Account_In.date
            };

            RabbitMq rabbitMq = new RabbitMq();
            rabbitMq.SendMessage(users.id, "GetAvatarAccount");
            return Ok(profile);
        }
        catch (Exception ex)
        {
            return BadRequest("Выпало исключение: " + ex);
        }
    }
    /// <summary>
    /// Запрос на вывод списков аккаунтов
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllAccount()
    {
        try
        {
            List<RegistrationModel?> AutoModel = context.Account_Info.ToList();
            List<string> strings = new List<string>();
            foreach (RegistrationModel? model in AutoModel)
                strings.Add(model.username);
            return Ok(strings);
        }
        catch (Exception ex)
        {
            return BadRequest("Выпало исключение: " + ex);
        }
    }
    /// <summary>
    /// Запрос на авторизацию через JWT-Token
    /// </summary>
    [HttpGet]
    public IActionResult GetTokenAccount(AccountTemplate account)
    {
        try
        {
            ModuleHash moduleHash = new ModuleHash();
            string passHash = moduleHash.GetHash(account.password);
            AutorizationModel? AutoModel = context.Account.FirstOrDefault(a => a.email == account.Email && a.password == passHash);
            if (AutoModel is null)
            {
                logger.LogWarning($"Модель была пустой (AutorizationModel):"+"Time: {DateTime.Now.ToLongTimeString()}");
                return BadRequest("Не найден аккаунт");
            }

            UserModel? users = context.User.FirstOrDefault(a => a.id_account == AutoModel.id);
            if (users is null)
            {
                logger.LogWarning($"Модель была пустой (UserModel):" + "Time: {DateTime.Now.ToLongTimeString()}");
                return BadRequest("Не найден ключ");
            }
            RegistrationModel? Account_In = context.Account_Info.FirstOrDefault(a => a.id == users.id_info);
            if (Account_In is null)
            {
                logger.LogWarning($"Модель была пустой (RegistrationModel):" + "Time: {DateTime.Now.ToLongTimeString()}");
                return BadRequest("Не найдены записи об аккаунте");
            }
                
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name,AutoModel.email) 
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,"Cookies");
            var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(60)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                user = Account_In.username
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            var json = JsonConvert.SerializeObject(ex)!;
            return BadRequest("Выпало исключение: " +  json);
        }
    }
    /// <summary>
    /// Запрос на создание аккаунта
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> RegistrationAccountImage(FullAccountTemplate accountTemplate)
    {
        try
        {
            Guid info = Guid.NewGuid();
            Guid account = Guid.NewGuid();
            logger.LogInformation("[Process...] Create Info User");
            RegistrationModel? registrationModel = context.Account_Info.FirstOrDefault(a => a.username == accountTemplate.UserName);
            if (registrationModel is not null)
            {
                logger.LogWarning("[Stop!] Select one Info User:"+accountTemplate.UserName);
            }
            else
            {
                RegistrationModel? RegModel = new RegistrationModel
                {
                    username = accountTemplate.UserName,
                    date = (DateTime)accountTemplate.Dateofbirths
                };
                info = RegModel.id;
                context.Account_Info.AddRange(RegModel);
                context.SaveChanges();
            }

            logger.LogInformation("[Process...] Create Account User");
            ModuleHash moduleHash = new ModuleHash();
            string HashPass = moduleHash.GetHash(accountTemplate.password);
            AutorizationModel? autoreg = context.Account.FirstOrDefault(a => a.email == accountTemplate.Email);
            Console.WriteLine("[Process...Hash?] " + HashPass);
            if (autoreg is not null)
            {
                logger.LogWarning("[Stop!] Select one Account User:"+accountTemplate.Email);
            }
            else
            {
                AutorizationModel? AutoModel = new AutorizationModel
                {
                    email = accountTemplate.Email,
                    password = HashPass
                };
                context.Account.AddRange(AutoModel);
                account = AutoModel.id;
                context.SaveChanges();
            }
            logger.LogInformation("[Process...] Create User");
            UserModel? user = context.User.FirstOrDefault(a => a.id_info == info && a.id_account == account);
            if (user is not null)
            {
                logger.LogWarning("[Stop!] Select one Account User");
            }
            else
            {
                UserModel? users = new UserModel()
                {
                    id_info = info,
                    id_account = account,
                };
                RabbitMq rabbitMQ = new RabbitMq();
                context.User.AddRange(users);
                context.SaveChanges();
                rabbitMQ.SendMessage(users.id, "CreateAvatarAccount");
            }
            return Ok("Регистрация пользователя");
        }
        catch (Exception ex)
        {
            return BadRequest("Выпало исключение: " + ex);
        }
    }
    /// <summary>
    /// Запрос на изменение аккаунта
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> UpdateAccount(string oldUser, FullAccountTemplate modelAccount)
    {
        try
        {
            RegistrationModel? Account_In = context.Account_Info.FirstOrDefault(a => a.username == oldUser);
            if (Account_In is null)
            {
                logger.LogWarning($"Модель была пустой (RegistrationModel):" + "Time: {DateTime.Now.ToLongTimeString()}");
                return BadRequest("Не найдены записи об аккаунте");
            }
            UserModel? users = context.User.FirstOrDefault(a => a.id_info == Account_In.id);
            if (users is null)
            {
                logger.LogWarning($"Модель была пустой (UserModel):" + "Time: {DateTime.Now.ToLongTimeString()}");
                return BadRequest("Не найден ключ");
            }
            AutorizationModel? AutoModel = context.Account.FirstOrDefault(a => a.id == users.id_account);
            if (AutoModel is null)
            {
                logger.LogWarning($"Модель была пустой (AutorizationModel):" + "Time: {DateTime.Now.ToLongTimeString()}");
                return BadRequest("Не найден аккаунт");
            }


            if (modelAccount.UserName != null && modelAccount.UserName != "")
                Account_In.username = modelAccount.UserName;
            else
                Account_In.username = Account_In.username;

            if (modelAccount.Email != null && modelAccount.Email != "")
                AutoModel.email = modelAccount.Email;
            else
                AutoModel.email = AutoModel.email;

            if (modelAccount.password != null && modelAccount.password != "")
            {
                ModuleHash moduleHash = new ModuleHash();
                string passHash = modelAccount.password;
                AutoModel.password = modelAccount.password;
            }
            else
                AutoModel.password = AutoModel.password;

            if (modelAccount.Dateofbirths != null)
                Account_In.date = (DateTime)modelAccount.Dateofbirths;
            else
                Account_In.date = Account_In.date;

            await context.SaveChangesAsync();
            RabbitMq rabbit = new RabbitMq();
            rabbit.SendMessage(users.id,"UpdateAvatarAccount");
            return Ok("Обновления применены");
        }
        catch (Exception ex)
        {
            return BadRequest("Выпало исключение: "+ ex);
        }
    }
    /// <summary>
    /// Запрос на удаление аккаунта
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> DeleteAccount(string userName)
    {
        try
        {
            RegistrationModel? Account_In = context.Account_Info.FirstOrDefault(a => a.username == userName);
            if (Account_In is null)
            {
                logger.LogWarning($"Модель была пустой (RegistrationModel):" + "Time: {DateTime.Now.ToLongTimeString()}");
                return BadRequest("Не найдены записи об аккаунте");
            }
            UserModel? users = context.User.FirstOrDefault(a => a.id_info == Account_In.id);
            if (users is null)
            {
                logger.LogWarning($"Модель была пустой (UserModel):" + "Time: {DateTime.Now.ToLongTimeString()}");
                return BadRequest("Не найден ключ");
            }
            AutorizationModel? AutoModel = context.Account.FirstOrDefault(a => a.id == users.id_account);
            if (AutoModel is null)
            {
                logger.LogWarning($"Модель была пустой (AutorizationModel):" + "Time: {DateTime.Now.ToLongTimeString()}");
                return BadRequest("Не найден аккаунт");
            }

            context.User.Remove(users);
            context.Account.Remove(AutoModel);
            context.Account_Info.Remove(Account_In);
            await context.SaveChangesAsync();
            RabbitMq rabbit = new RabbitMq();
            rabbit.SendMessage(users.id, "DeleteAvatarAccount");
            return Ok("Удален пользователь с ником: "+ userName);
        }
        catch (Exception ex)
        {
            return BadRequest("Выпало исключение: " + ex);
        }
    }
}