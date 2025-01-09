namespace DataBasePostgres.Models.AutoReg;

public class RegistrationModel
{
    public Guid id { get; set; }
    public string username { get; set; }
    public DateTime dateofbirths { get; set; }

    public RegistrationModel()
    {
        id = Guid.NewGuid();
    }

}
