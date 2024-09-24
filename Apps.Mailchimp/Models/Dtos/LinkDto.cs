namespace Apps.Mailchimp.Models.Dtos;

public class LinkDto
{
    public string Rel { get; set; } = default!;
    
    public string Href { get; set; } = default!;
    
    public string Method { get; set; } = default!;
    
    public string TargetSchema { get; set; } = default!;
    
    public string Schema { get; set; } = default!;
}