namespace PAPI_Libs.Models;

public class PAPI_Lib
{
    public int Id { get; set; }
    public string PAPI_LibTemplate { get; set; }
    public string OriginalQuote { get; set; }
    public string OriginalQuoteAuthorOrSource { get; set; }
    public int TemplateId { get; set; }
    public string? CompletedString { get; set; }
    public string ApiUrlOne { get; set; }
    public string ApiNameOne { get; set; }
    public string? ApiUrlTwo { get; set; }
    public string? ApiNameTwo { get; set; }
}