namespace cw11.DTOs;

public class CreateBedAssignmentDto
{
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    public string BedType { get; set; }
    public string Ward { get; set; }
}