namespace PAPI_Libs.Models;
using Microsoft.EntityFrameworkCore;

public class PAPI_LibTemplate
{
    public int Id { get; set; }
    public string TemplateString { get; set; }
    public string OriginalQuote { get; set; }
    public string OriginalQuoteAuthorOrSource { get; set; }
    public string ApiUrlOne { get; set; }
    public string ApiNameOne { get; set; }
    public string? ApiUrlTwo { get; set; }
    public string? ApiNameTwo { get; set; }
}