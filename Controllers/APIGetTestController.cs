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
    [HttpGet]
    public async Task<ActionResult<string>> Get()
    {
        string url = "https://corporatebs-generator.sameerkumar.website/";
        using (HttpClient client = new HttpClient())
        {
            return await client.GetStringAsync(url);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<string>> Get(int id)
    {
        string url=$"https://fruityvice.com/api/fruit/{id}";
        using (HttpClient client = new HttpClient())
        {
            return await client.GetStringAsync(url);
        }
    }

}