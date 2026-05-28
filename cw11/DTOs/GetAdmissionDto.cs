namespace cw11.DTOs;

public class GetAdmissionDto
{
    public int Id { get; set; }
    public DateTime AdmissionDate { get; set; }
    public DateTime DischargeDate { get; set; }
    public string PatientPesel { get; set; }
    
    public GetWard Ward { get; set; }
}