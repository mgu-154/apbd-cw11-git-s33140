using cw11.DTOs;
using cw11.Exceptions;
using cw11.Services;
using Microsoft.AspNetCore.Mvc;

namespace cw11.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IDbService _dbService;

    public PatientsController(IDbService dbService)
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

        var patients = await _dbService.GetSearchedPatients(text);

        if (!patients.Any())
        {
            return NotFound("Taki pacjent nie istnieje");
        }
        
        return Ok(patients);
    }

    [HttpPost("{pesel}/bedassignments")]
    public async Task<IActionResult> CreateBedAssignment(string pesel, CreateBedAssignmentDto bedAssignment)
    {
        try
        {
           await _dbService.CreateBedAssignment(pesel, bedAssignment);

           return Created(nameof(CreateBedAssignment), bedAssignment);
        } 
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}