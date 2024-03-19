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
    /* (3/18/24, 4) I wonder what would happen if we took out the parameter (i.e., `PAPI_Lib papi_lib`) passed 
    to the `Post` method...? */
    /* (3/18/24, 5) BINGO. PROGRESS! */
    public IActionResult Post()
    {            
        PAPI_Lib papi_lib = new PAPI_Lib();
        PAPI_LibService.Add(papi_lib);
        return CreatedAtAction(nameof(Get), new { id = papi_lib.Id }, papi_lib);
    }

    // PUT action
    [HttpPut("{id}")]
    public IActionResult Update(int id, PAPI_Lib papi_lib)
    {
        if (id != papi_lib.Id)
            return BadRequest();
            
        var existingPAPI_Lib = PAPI_LibService.Get(id);
        if(existingPAPI_Lib is null)
            return NotFound();
    
        PAPI_LibService.Update(papi_lib);           
    
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