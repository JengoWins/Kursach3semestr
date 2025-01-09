namespace Login_Account.Models.AutoReg;

public class RegistrationModel
{
    public Guid id { get; set; }
    public string username { get; set; }
    public DateTime date { get; set; }

    public RegistrationModel()
    {
        id = Guid.NewGuid();
    }

}
