// (3/3/24, 1) To be populated!

/* (3/3/24, 2) Yeah... Come to think of it, it MIGHT be the case that this tutorial you are following 
will NOT have you implement Entity Framework Core like you had hoped (or at least it SEEMS this way as 
of "Unit 5 of 9" in the tutorial 
(https://learn.microsoft.com/en-us/training/modules/build-web-api-aspnet-core/5-exercise-add-data-store). 
As such, IF/WHEN you finish this tutorial and find that the "in-memory service" you are creating in 
"PizzaService.cs" is insufficient, you have two "next step" options of immediate resort:
    1. Check Ernesto Ramos's GitHub ("bumbolio" or something like that) for the projects that he has been 
    making during our weekly meet-ups. I could almost swear that he DID make an ASP.NET Core Web API project 
    that DID use EF Core and PERHAPS had an MVC front-end...? 
    2. Take a look at the Microsoft tutorial located at 
    https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&preserve-view=true&tabs=visual-studio-code
    and - for lack of a prettier way of saying it - cannibalize the parts of it having to do with EF Core, 
    taking EXCEPTIONAL CARE to ensure, TO THE BEST OF YOUR ABILITY, that your inclusion of EF Core DOES NOT 
    conflict with the "working" operations within the PAPI-Libs "app" at that point in time. */

/* (3/17/24, 1) IDEA: Move ALL the API-specific `GET` requests to THIS "PAPI_LibService.cs" file to clean-up what WILL become 
the "PAPI_LibController.cs" controller? */

using PAPI_Libs.Models;

namespace PAPI_Libs.Services;

/* (3/17/24, 2) At this point, I consider most all the service-level infrastructure defined below to be more of a "proof-of-
concept" that allows me to build-out the core "business logic" underlying the app. Once I have the business logic more-or-less 
figured out, I will, of course, back-up my work via Git, but once that is done, I IMAGINE that a lot of what is here will be 
deleted OR OTHERWISE heavily edited: */
public static class PAPI_LIBService
{
    static List<PAPI_Lib> PAPI_Libs { get; }
    static int nextId = 3;

    /* (3/17/24, 4) IDEA: FOR THE TIME BEING, SHOULD I create a "PAPI_LibTemplates" List from which any sort of a `POST` action 
    (in "PAPI_LibController.cs" and/or `Add` method (in "PAPI_LibService.cs") should pull?! */
    static PAPI_LIBService()
    {
        PAPI_Libs = new List<PAPI_Lib>
        {
            new PAPI_Lib
            {
                Id = 1,
                PAPI_LibTemplate = "When life gives you {fruit}, {corporateBs}.",
                OriginalQuote = "When life gives you lemons, make lemonade.",
                OriginalQuoteAuthor = "Common English Proverb",
                TemplateId = 1,
                CompletedString = "When life gives you durian, completely envisioneer frictionless sprints.",
                ApiUrlOne = "https://www.fruityvice.com/api/fruit/",
                ApiNameOne = "Fruityvice API",
                ApiUrlTwo = "https://corporatebs-generator.sameerkumar.website/",
                ApiNameTwo = "Corporate Buzzword Generator API"
            },
            new PAPI_Lib
            {
                Id = 2,
                PAPI_LibTemplate = "{musicGenre} makes the heart grow fonder.",
                OriginalQuote = "Absence makes the heart grow fonder.",
                OriginalQuoteAuthor = "Common English Proverb",
                TemplateId = 2,
                CompletedString = "Hawaiian Cornet Revival makes the heart grow fonder.",
                ApiUrlOne = "https://binaryjazz.us/wp-json/genrenator/v1/genre/",
                ApiNameOne = "Genrenator API (`/genre/` endpoint)",
            }
        };
    }

    public static List<PAPI_Lib> GetAll() => PAPI_Libs;

    public static PAPI_Lib? Get(int id) => PAPI_Libs.FirstOrDefault(p => p.Id == id);

    public static void Add(PAPI_Lib papi_lib)
    {
        papi_lib.Id = nextId++;
        PAPI_Libs.Add(papi_lib);
    }

    public static void Delete(int id)
    {
        var papi_lib = Get(id);
        if(papi_lib is null)
            return;

        PAPI_Libs.Remove(papi_lib);
    }

    public static void Update(PAPI_Lib papi_lib)
    {
        var index = PAPI_Libs.FindIndex(p => p.Id == papi_lib.Id);
        if(index == -1)
            return;

        PAPI_Libs[index] = papi_lib;
    }
}