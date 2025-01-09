using System.ComponentModel.DataAnnotations.Schema;

namespace EnergencyService.Models.AutoReg;

public class RegistrationModel
{
    public Guid id { get; set; }
    [Column(TypeName = "varchar(150)")]
    public string username { get; set; }
    public DateTime date { get; set; }

    public RegistrationModel()
    {
        id = Guid.NewGuid();
    }

}
