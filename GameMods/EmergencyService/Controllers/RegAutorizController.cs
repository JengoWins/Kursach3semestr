using Microsoft.AspNetCore.Mvc;
using EnergencyService.DataBaseClasses;
using EnergencyService.RabbitMQ;



namespace EnergencyService.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RegistrationController : ControllerBase
{
    private ApplicationContext context;
    private readonly ILogger logger;

    public RegistrationController(ApplicationContext context, ILogger<RegistrationController> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetReLoadController()
    {
        return Ok("So, I'm job in Controller. ТЕСТИРОВАНИЕ ЗАВЕРШЕНО");
    }

    [HttpPost]
    public async Task<IActionResult> RegistrationAccount()
    {
        try
        {
            RabbitMq rabbit = new RabbitMq();
            bool isCheck = await rabbit.ReceiveMessage("User");
            if (!isCheck)
                logger.LogInformation("Аккаунты занесены");
            else
                logger.LogInformation("Выпало исключение: Возможен случай существование некоторых ряда пользователь");
            return Ok("Аккаунты занесен в базу");
        }
        catch (Exception ex)
        {
            return BadRequest("Выпало исключение: " + ex);
        }
    }
}