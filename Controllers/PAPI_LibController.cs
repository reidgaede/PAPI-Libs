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
        PAPI_LIBService.GetAll();

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<PAPI_Lib> Get(int id)
    {
        var papi_lib = PAPI_LIBService.Get(id);

        if(papi_lib == null)
            return NotFound();

        return papi_lib;
    }

    // POST action
    [HttpPost]
    public IActionResult Create(PAPI_Lib papi_lib)
    {            
        PAPI_LIBService.Add(papi_lib);
        return CreatedAtAction(nameof(Get), new { id = papi_lib.Id }, papi_lib);
    }

    // PUT action
    [HttpPut("{id}")]
    public IActionResult Update(int id, PAPI_Lib papi_lib)
    {
        if (id != papi_lib.Id)
            return BadRequest();
            
        var existingPAPI_Lib = PAPI_LIBService.Get(id);
        if(existingPAPI_Lib is null)
            return NotFound();
    
        PAPI_LIBService.Update(papi_lib);           
    
        return NoContent();
    }

    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var papi_lib = PAPI_LIBService.Get(id);
    
        if (papi_lib is null)
            return NotFound();
        
        PAPI_LIBService.Delete(id);
    
        return NoContent();
    }
}