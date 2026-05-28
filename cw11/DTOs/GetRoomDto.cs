namespace cw11.DTOs;

public class GetRoomDto
{
    public string Id { get; set; }
    public bool HasTv { get; set; }
    
    public GetWardDto Ward { get; set; }
}