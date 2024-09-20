﻿namespace Apps.Mailchimp.Models.Dtos;

public class ErrorDto
{
    public string Type { get; set; } = default!;
    
    public string Title { get; set; } = default!;
    
    public int Status { get; set; }
    
    public string Detail { get; set; } = default!;
    
    public string Instance { get; set; } = default!;
    
    public override string ToString()
    {
        return $"Type: {Type}, Title: {Title}, Status: {Status}, Detail: {Detail}, Instance: {Instance}";
    }
}