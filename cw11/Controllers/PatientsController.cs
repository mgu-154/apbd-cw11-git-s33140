using cw11.Exceptions;
using cw11.Services;
using Microsoft.AspNetCore.Mvc;

namespace cw11.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly IDbService _dbService;

    public PatientController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPatients()
    {
        var patients = await _dbService.GetAllPatients();
        
        return Ok(patients);
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetSearchedPatients(string text)
    {
        try
        {
            var patients = await _dbService.GetSearchedPatients(text);
            
            return Ok(patients);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        
    }
}