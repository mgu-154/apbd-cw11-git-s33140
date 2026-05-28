using cw11.Data;
using cw11.DTOs;
using cw11.Exceptions;
using cw11.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cw11.Services;

public class DbService : IDbService
{
    private readonly DbfirstContext _context;
    public DbService(DbfirstContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<GetPatientDto>> GetAllPatients()
    {
        var res = await _context.Patients
            .Select(e => new GetPatientDto()
            {
                Pesel = e.Pesel,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Age = e.Age,
                Sex = e.Sex,
                Admissions = e.Admissions.Select(e => new GetAdmissionDto()
                {
                    Id = e.Id,
                    AdmissionDate = e.AdmissionDate,
                    DischargeDate = e.DischargeDate,
                    Ward = new GetWardDto()
                    {
                        Id = e.Ward.Id,
                        Name = e.Ward.Name,
                        Description = e.Ward.Description
                    }
                }).ToList(),
                BedAssignments = e.BedAssignments.Select(e => new GetBedAssignmentDto()
                {
                    Id = e.Id,
                    From = e.From,
                    To = e.To,
                    Bed = new GetBedDto()
                    {
                        Id = e.Bed.Id,
                        BedType = new GetBedTypeDto()
                        {
                            Id = e.Bed.BedType.Id,
                            Name = e.Bed.BedType.Name,
                            Description = e.Bed.BedType.Description
                        },
                        Room = new GetRoomDto()
                        {
                            Id = e.Bed.Room.Id,
                            HasTv = e.Bed.Room.HasTv,
                            Ward = new GetWardDto()
                            {
                                Id = e.Bed.Room.Ward.Id,
                                Name = e.Bed.Room.Ward.Name,
                                Description = e.Bed.Room.Ward.Description
                            }
                        }
                    }
                }).ToList()
            }).ToListAsync();

        return res;
    }

    public async Task<IEnumerable<GetPatientDto>> GetSearchedPatients(
        [FromQuery] string text)
    {
        var res = await _context.Patients
            .Where(p => p.FirstName.Contains(text) || p.LastName.Contains(text))
            .Select(e => new GetPatientDto()
            {
                Pesel = e.Pesel,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Age = e.Age,
                Sex = e.Sex,
                Admissions = e.Admissions.Select(e => new GetAdmissionDto()
                {
                    Id = e.Id,
                    AdmissionDate = e.AdmissionDate,
                    DischargeDate = e.DischargeDate,
                    Ward = new GetWardDto()
                    {
                        Id = e.Ward.Id,
                        Name = e.Ward.Name,
                        Description = e.Ward.Description
                    }
                }).ToList(),
                BedAssignments = e.BedAssignments.Select(e => new GetBedAssignmentDto()
                {
                    Id = e.Id,
                    From = e.From,
                    To = e.To,
                    Bed = new GetBedDto()
                    {
                        Id = e.Bed.Id,
                        BedType = new GetBedTypeDto()
                        {
                            Id = e.Bed.BedType.Id,
                            Name = e.Bed.BedType.Name,
                            Description = e.Bed.BedType.Description
                        },
                        Room = new GetRoomDto()
                        {
                            Id = e.Bed.Room.Id,
                            HasTv = e.Bed.Room.HasTv,
                            Ward = new GetWardDto()
                            {
                                Id = e.Bed.Room.Ward.Id,
                                Name = e.Bed.Room.Ward.Name,
                                Description = e.Bed.Room.Ward.Description
                            }
                        }
                    }
                }).ToList()
            }).ToListAsync();

        return res;
    }

    public async Task CreateBedAssignment(string pesel, CreateBedAssignmentDto bedDto)
    {
        var findBed = await _context.Beds
            .Where(b => b.BedType.Name == bedDto.BedType)
            .Where(b => b.Room.Ward.Name == bedDto.Ward)
            .Where(b => !b.BedAssignments
                .Any(ba => ba.From < bedDto.To && ba.To > bedDto.From))
            .FirstOrDefaultAsync();

        if (findBed is null)
        {
            throw new NotFoundException("Nie znaleziono wolnego lozka na podanym oddziale");
        }
        
        var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var assignment = new BedAssignment()
            {
                PatientPesel = pesel,
                BedId = findBed.Id,
                From = bedDto.From,
                To = bedDto.To,
            };
        
            await _context.BedAssignments.AddAsync(assignment);
            await _context.SaveChangesAsync();
            
            await transaction.CommitAsync();
            
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}