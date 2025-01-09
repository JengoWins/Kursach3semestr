using Microsoft.AspNetCore.Mvc;

namespace Posts.Models.Template;

public class FullPostTemplate
{
    public string namePost { get; set; }
    public string typeGame { get; set; }
    public string Process { get; set; }
    public string miniDescriptionPost { get; set; }
    public string descriptionPost { get; set; }
    public DateTime date { get; set; }
    public string user { get; set; }
}

public class PostTemplate
{
    [FromQuery]
    public string namePost { get; set; }

    [FromQuery]
    public string typeGame { get; set; }

    [FromQuery]
    public string Process { get; set; }

    [FromQuery]
    public string miniDescriptionPost { get; set; }
    
    [FromQuery]
    public string descriptionPost { get; set; }
}
