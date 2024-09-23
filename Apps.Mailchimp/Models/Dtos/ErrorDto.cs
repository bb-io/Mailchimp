namespace Apps.Mailchimp.Models.Dtos;

public class ErrorDto
{
    public string Type { get; set; } = default!;
    
    public string Title { get; set; } = default!;
    
    public int Status { get; set; }
    
    public string Detail { get; set; } = default!;
    
    public string Instance { get; set; } = default!;

    public List<FieldDto> Errors { get; set; }
    
    public override string ToString()
    {
        var message = $"Type: {Type}, Title: {Title}, Status: {Status}, Detail: {Detail}, Instance: {Instance}";
        if (Errors.Any())
        {
            message += $", Errors: {string.Join(", ", Errors.Select(x => $"Field: {x.Field}, Message: {x.Message}"))}";
        }
        
        return message;
    }
}

public class FieldDto
{
    public string Field { get; set; } = default!;
    
    public string Message { get; set; } = default!;
}