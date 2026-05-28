using cw11.Models;

namespace cw11.DTOs;

public class GetBedDto
{
    public int Id { get; set; }
    public GetBedTypeDto BedType { get; set; }
    public GetRoomDto Room { get; set; }
}