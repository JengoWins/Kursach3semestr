namespace Login_Account.Models.AutoReg;

public class AutorizationModel
{
    public Guid id { get; set; }
    public string? email { get; set; }
    public string? password { get; set; }
    public AutorizationModel()
    {
        id = Guid.NewGuid();
    }
}
