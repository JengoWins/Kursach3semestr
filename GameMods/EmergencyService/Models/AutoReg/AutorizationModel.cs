using System.ComponentModel.DataAnnotations.Schema;

namespace EnergencyService.Models.AutoReg;

public class AutorizationModel
{
    public Guid id { get; set; }
    [Column(TypeName = "varchar(150)")]
    public string? email { get; set; }
    [Column(TypeName = "varchar(150)")]
    public string? password { get; set; }
    public AutorizationModel()
    {
        id = Guid.NewGuid();
    }
}
