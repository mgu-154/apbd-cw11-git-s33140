namespace cw11.DTOs;

public class GetRoomDto
{
    public int Id { get; set; }
    public bool HasTv { get; set; }
    
    public GetWard Ward { get; set; }
}