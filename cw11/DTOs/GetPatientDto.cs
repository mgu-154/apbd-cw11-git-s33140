namespace cw11.DTOs;

public class GetPatientDto
{
    public string Pesel { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public bool Sex { get; set; }
    
    public List<GetAdmissionDto> Admissions { get; set; }
    public List<GetBedAssignmentDto> BedAssignments { get; set; }
}