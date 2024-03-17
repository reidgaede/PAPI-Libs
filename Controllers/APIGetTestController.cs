using PAPI_Libs.Models;
using PAPI_Libs.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace PAPI_Libs.Controllers;

[ApiController]
[Route("[controller]")]
public class APIGetTestController : ControllerBase
{
    public APIGetTestController()
    {
    }
    
    /* (3/4/24, 3) Commenting-out this entire "controller action" since (at least to my knowledge?), my goal 
    has NEVER been to get EVERY single entry in one of the publicly-accessible APIs I am working with. INSTEAD, 
    my hope is to obtain a SINGLE entry from a given API ON THE BASIS of a random integer generator. AS SUCH, 
    the next step is to SEE, AT A SIMPLE LEVEL, if it is EVEN POSSIBLE to simply obtain a SINGLE ENTRY from the 
    "Fruityvice" API ON THE BASIS OF INTEGER-BASED ID VALUE (can you tell that I am excited?): */
    /* [HttpGet]
    public async Task<ActionResult<string>> Get()
    { */
        /* (3/4/24, 1) THIS WORKS!!! Let's try it with the "Fruityvice" API returning ALL the fruits it 
        details (!...?): */
        // string url="https://jsonplaceholder.typicode.com/todos";

        /* (3/4/24, 2) THIS WORKS TOO!!! Let's try it with the "Fruityvice" API returning a SINGLE fruit 
        on the basis of ID value (!...?): */
        /* string url="https://fruityvice.com/api/fruit/all";
        using (HttpClient client = new HttpClient())
        {
            return await client.GetStringAsync(url);
        }
    } */

    /* (3/4/24, 4) The Stack Overflow thread at the following URL was CRUCIAL to the "progress" I made above: 
    https://stackoverflow.com/questions/55359047/how-to-make-http-call-from-controller-to-use-web-apis-asp-net-core-c-sharp */

    /* (3/4/24, 5) This works, too! That means that out of the five publicly-accessible APIs you selected, AT LEAST two 
    work in SOME capacity (!): */
    [HttpGet("/corporatebs/")]
    public async Task<ActionResult<string>> Get()
    {
        string url = "https://corporatebs-generator.sameerkumar.website/";
        using (HttpClient client = new HttpClient())
        {
            return await client.GetStringAsync(url);
        }
    }

    [HttpGet("/fruityvice/{id}")]
    public async Task<ActionResult<string>> Get(int id)
    {
        string url=$"https://fruityvice.com/api/fruit/{id}";
        using (HttpClient client = new HttpClient())
        {
            return await client.GetStringAsync(url);
        }
    }

    /* (3/5/24) CONSIDER writing-out the rest of the `GET` methods here - one for EACH of the publicly-acessible APIs (you SHOULD be able to access them via HTTP REPL AS IF 
    they were separate directories via the `cd` command!...?): */

    /* (3/16/24, 1) Let's test "getting" from the OTHER publicly-accessible APIs here (HOPEFULLY this will be WITHOUT INCIDENT (...?): */
    
    // (3/16/24, 2) Testing out the "genre" endpoint of the "Genrenator API":
    [HttpGet("/genrenator_genre/")]
    /* (3/16/24, 6) Error message returned: "An unhandled exception has occurred while executing the request. Swashbuckle.AspNetCore.SwaggerGen.SwaggerGeneratorException: 
    Conflicting method/path combination "GET APIGetTest" for actions - 
    PAPI_Libs.Controllers.APIGetTestController.Get (PAPI-Libs),
    PAPI_Libs.Controllers.APIGetTestController.GetGenre (PAPI-Libs),
    PAPI_Libs.Controllers.APIGetTestController.GetMusicStory (PAPI-Libs).
    Actions require a unique method/path combination for Swagger/OpenAPI 3.0. Use ConflictingActionsResolver as a workaround". 
    
    I ultimately ended up "fixing" this issue by making additional "route specifications" in each of the `[HttpGet]` attributes in this controller (i.e., I added additional, 
    COMPLETELY ARBITRARY path information to each `[HttpGet]` attribute. Doing so prevents Swagger from confusing six different `GET` requests with each other). Rather simple, 
    but took me longer than I care to admit to figure out. At any rate, that's why every `[HttpGet]` attribute in this file now looks like `[HttpGet("genrenator_story/")]`, 
    `[HttpGet("Gutendex/{id}")]`, etc. 
    
    The Stack Overflow answer(s?) available at the following URL was (were?) crucial to solving this issue: 
    https://stackoverflow.com/questions/59283210/how-do-i-resolve-the-issue-the-request-matched-multiple-endpoints-in-net-core-w*/
    public async Task<ActionResult<string>> GetGenre()
    {
        string url = "https://binaryjazz.us/wp-json/genrenator/v1/genre/";
        using (HttpClient client = new HttpClient())
        {
            return await client.GetStringAsync(url);
        }
    }

    // (3/16/24, 3) Testing out the "story" endpoint of the "Genrenator API":
    [HttpGet("genrenator_story/")]
    public async Task<ActionResult<string>> GetMusicStory()
    {
        string url = "https://binaryjazz.us/wp-json/genrenator/v1/story/";
        using (HttpClient client = new HttpClient())
        {
            return await client.GetStringAsync(url);
        }
    }

    // (3/16/24, 4) Testing out the "Gutendex" API:
    [HttpGet("Gutendex/{id}")]
    public async Task<ActionResult<string>> GetBook(int id)
    {
        string url=$"https://gutendex.com/books/{id}";
        using (HttpClient client = new HttpClient())
        {
            return await client.GetStringAsync(url);
        }
    }

    // (3/16/24, 5) Testing out The Metropolitan Museum of Art's "Collection API":
    [HttpGet("ArtAPI/{id}")]
    public async Task<ActionResult<string>> GetArt(int id)
    {
        string url=$"https://collectionapi.metmuseum.org/public/collection/v1/objects/{id}";
        using (HttpClient client = new HttpClient())
        {
            return await client.GetStringAsync(url);
        }
    }

    /* (3/16/24, 7) After EXCESSIVE troubleshooting to figure out why the heck controller routes were getting tangled, all five 
    publicly-accessible APIs HAVE been tested and ARE confirmed to be reasonably functional!
    
    NEXT STEP: build those models and get to work on building an EF Core database for your project!*/
}