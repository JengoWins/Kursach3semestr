using Microsoft.AspNetCore.Mvc;

namespace Posts.Models.Template;

public class CategoryGameTemplate
{
    [FromQuery]
    public string category { get; set; }
}
