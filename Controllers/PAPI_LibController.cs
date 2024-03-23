/* (3/17/24, 3) STARTING to get this set-up just so that I can start putting more pieces into place (note that as currently 
imagined, ALL the `GET` ACTIONS SHOWN IN THE "APIGetTestControoler.cs" file WILL be moved to "PAPI_LibService.cs" UNLESS doing so 
proves to be overly-challenging. */

using PAPI_Libs.Models;
using PAPI_Libs.Services;
using Microsoft.AspNetCore.Mvc;

namespace PAPI_Libs.Controllers;

[ApiController]
[Route("[controller]")]
public class PAPI_LibController : ControllerBase
{
    public PAPI_LibController()
    {
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<PAPI_Lib>> GetAll() =>
        PAPI_LibService.GetAll();

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<PAPI_Lib> Get(int id)
    {
        var papi_lib = PAPI_LibService.Get(id);

        if(papi_lib == null)
            return NotFound();

        return papi_lib;
    }

    // POST action
    [HttpPost]
    /* (3/19/24, 4) Updated this method's/action's signature in order to make it PROPERLY "asynchronous" (!): */
    public async Task<IActionResult> Post()
    {            
        PAPI_Lib papi_lib = new PAPI_Lib();
        await PAPI_LibService.Add(papi_lib);
        return CreatedAtAction(nameof(Get), new { id = papi_lib.Id }, papi_lib);
    }

    // PUT action
    [HttpPut("{id}")]
    /* (3/22/24, 1) So, as the `PUT` action CURRENTLY exists, it accepts BOTH an `id` value AND a `PAPI_Lib` object 
    AS INPUT. Currently, I THINK that I want it so that the `Update()` method (which SERVES as a `PUT` action) to ONLY 
    accept an `id` value so that users themselves DO NOT have the option to specify themselves what the `PAPI_Lib`'s 
    updated property values are going to be. AS SUCH, I think my first step here is going to be to re-write this method 
    so that it can function WITHOUT being passed a `PAPI_Lib` object (...): */
    public async Task<IActionResult> Update(int id)
    {
        /* (3/22/24, 2) Not rightly sure how to deal with this, so just commenting it out for now: */
        //if (id != papi_lib.Id)
            //return BadRequest();
            
        var existingPAPI_Lib = PAPI_LibService.Get(id);
        if(existingPAPI_Lib is null)
            return NotFound();
    
        /* (3/22/24, 3) Alright, we MIGHT be starting to get somewhere. From here, I THINK that we basically want to "re-
        write" the `Update()` method found in "PAPI_LibService.cs" so that it no longer takes a `PAPI_Lib` object as its 
        input, but RATHER the `id` value from the `Update()` method in "PAPI_LibController.cs". The reason for this is that 
        since users (by my (crazy) design) will NOT be able to DIRECTLY update the values of a chosen `PAPI_Lib` object 
        themselves, there is no need for any sort of interaction with the `PAPI_Lib` object in question to take palce within 
        the controller itself (...I think?)! INSTEAD, ALL that stuff with OBTAINING the `PAPI_Lib` object to be updated, 
        RANDOMLY updating it, etc. is done within "PAPI_LibService.cs" (...I think?). LONG STORY SHORT: go to 
        "PAPI_LibService.cs" and start re-writing the `Update()` method there so that while it does NOT take a `PAPI_Lib` 
        object as an input, it DOES FETCH the `PAPI_Lib` object with the matching `id` value and FROM THERE BASICALLY 
        re-executes the randomization process based on the `TemplateId` value or whatever (!):*/
        PAPI_LibService.Update(existingPAPI_Lib); // (3/22/24, 4) Doing this contradicts what I wrote above, but let's try!
        /* (3/22/24, 4) LOOKS LIKE your conceptual pivot visible in the line of code immediately above this comment DID 
        work! */           
    
        return NoContent();
    }

    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var papi_lib = PAPI_LibService.Get(id);
    
        if (papi_lib is null)
            return NotFound();
        
        PAPI_LibService.Delete(id);
    
        return NoContent();
    }
}