using PAPI_Libs.Models;
using PAPI_Libs.Services;
using Microsoft.AspNetCore.Mvc;

namespace PAPI_Libs.Controllers;

[ApiController]
[Route("[controller]")]
public class PAPI_LibController : ControllerBase
{
    PAPI_LibService _service;
    
    public PAPI_LibController(PAPI_LibService service)
    {
        _service = service;
    }

    // `GET [All]` action:
    [HttpGet]
    public ActionResult<List<PAPI_Lib>> GetAll() =>
        _service.GetAll().ToList();

    // `GET [by Id]` action:
    [HttpGet("{id}")]
    public ActionResult<PAPI_Lib> Get(int id)
    {
        var papi_lib = _service.GetById(id);

        if(papi_lib == null)
            return NotFound();

        return papi_lib;
    }

    // `POST` action:
    [HttpPost]
    public async Task<IActionResult> Post()
    {            
        PAPI_Lib papi_lib = new PAPI_Lib();
        await _service.Add(papi_lib);
        return CreatedAtAction(nameof(Get), new { id = papi_lib.Id }, papi_lib);
    }

    // `PUT` action:
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id)
    {
        var existingPAPI_Lib = _service.GetById(id);
        if(existingPAPI_Lib is null)
            return NotFound();
    
        await _service.Update(existingPAPI_Lib);        
    
        return NoContent();
    }

    // `DELETE` action:
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var papi_lib = _service.GetById(id);
    
        if (papi_lib is null)
            return NotFound();
        
        _service.Delete(id);
    
        return NoContent();
    }
}