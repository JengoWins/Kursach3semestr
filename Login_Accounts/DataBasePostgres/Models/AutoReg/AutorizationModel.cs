namespace DataBasePostgres.Models.AutoReg;

public class AutorizationModel
{
    public Guid id { get; set; }
    public Guid id_info { get; set; }
    public string? email { get; set; }
    public string? password { get; set; }
    public AutorizationModel()
    {
        id = Guid.NewGuid();
    }
}
