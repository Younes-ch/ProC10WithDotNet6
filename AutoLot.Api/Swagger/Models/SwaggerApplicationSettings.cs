namespace AutoLot.Api.Swagger.Models;

public class SwaggerApplicationSettings
{
    public string Title { get; set; }
    public List<SwaggerVersionDescription> Descriptions { get; set; } = [];
    public string ContactName { get; set; }
    public string ContactEmail { get; set; }
}
