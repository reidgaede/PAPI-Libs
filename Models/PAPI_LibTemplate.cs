namespace PAPI_Libs.Models;
/* (3/24/24, 7) Adding-in this "data annotation" capability so that we can TRY to identify `TemplateId` as the 
"primary key" of the "entity type" "PAPI_LibTemplate". The code for doing so was found under "Defining the Model" 
at https://code-maze.com/net-core-web-api-ef-core-code-first/, and was sought in response to an error that arose 
in-terminal when you tried to do a migration to create the initial TABLES in your database (for reference, the 
"important" (?) part of the returned error was "Unable to create a 'DbContext' of type 'PAPI_LibContext'. 
The exception 'The entity type 'PAPI_LibTemplate' requires a primary key to be defined."): */
/* using System.ComponentModel.DataAnnotations; (3/24/24, 8) NEVERMIND. Simply renaming `TemplateId` within the 
"PAPI_LibTemplate" Class to `Id` AND DOING THE SAME for All `TemplateId` References in "PAPI_LibService.cs". */
using Microsoft.EntityFrameworkCore;

public class PAPI_LibTemplate
{
    /* (3/24/24, 7) Adding-in this "data annotation" capability so that we can TRY to identify `TemplateId` as the 
    "primary key" of the "entity type" "PAPI_LibTemplate". The code for doing so was found under "Defining the Model" 
    at https://code-maze.com/net-core-web-api-ef-core-code-first/, and was sought in response to an error that arose 
    in-terminal when you tried to do a migration to create the initial TABLES in your database (for reference, the 
    "important" (?) part of the returned error was "Unable to create a 'DbContext' of type 'PAPI_LibContext'. 
    The exception 'The entity type 'PAPI_LibTemplate' requires a primary key to be defined."): */
    /* [Key] (3/24/24, 8) NEVERMIND. Simply renaming `TemplateId` within the  "PAPI_LibTemplate" Class to `Id` AND 
    DOING THE SAME for All `TemplateId` References in "PAPI_LibService.cs".*/
    public int Id { get; set; }
    public string TemplateString { get; set; }
    public string OriginalQuote { get; set; }
    public string OriginalQuoteAuthorOrSource { get; set; }
    public string ApiUrlOne { get; set; }
    public string ApiNameOne { get; set; }
    public string? ApiUrlTwo { get; set; }
    public string? ApiNameTwo { get; set; }
}