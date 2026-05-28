using cw11.DTOs;

namespace cw11.Services;

public interface IDbService
{
    Task<IEnumerable<GetPatientDto>> GetAllPatients();
    Task<IEnumerable<GetPatientDto>> GetSearchedPatients(string text);
    Task CreateBedAssignment(string pesel, CreateBedAssignmentDto bedAssignmentDto);
}