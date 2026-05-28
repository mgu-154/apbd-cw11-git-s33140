namespace cw11.DTOs;

public class GetBedAssignmentDto
{
    public int id { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    
    public GetBedDto BedDto { get; set; }
}