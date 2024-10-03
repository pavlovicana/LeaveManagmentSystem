﻿using AutoMapper;
using LeaveManagmentSystem.Web.Data;
using LeaveManagmentSystem.Web.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagmentSystem.Web.Services;

public class LeaveTypesService(ApplicationDbContext _context, IMapper _mapper) : ILeaveTypesService
{


    public async Task<List<LeaveTypeReadOnlyVM>> GetAll()
    {
        // var data = SELECT * FROM LeaveTypes
        var data = await _context.LeaveTypes.ToListAsync();
        // convert the datamodel into view model - Use AutoMapper
        var viewData = _mapper.Map<List<LeaveTypeReadOnlyVM>>(data);
        return viewData;
    }

    public async Task<T> Get<T>(int id) where T : class
    {
        var data = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);

        if (data == null)
        {
            return null;
        }

        var viewData = _mapper.Map<T>(data);
        return viewData;

    }

    public async Task Remove(int id)
    {
        var data = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);

        if (data != null)
        {
            _context.Remove(data);
            _context.SaveChanges();
        }
    }
    public async Task Edit(LeaveTypeEditVM model)
    {
        var leaveType = _mapper.Map<LeaveType>(model);
        _context.Update(leaveType);
        await _context.SaveChangesAsync();
    }
    public async Task Create(LeaveTypeCreateVM model)
    {
        var leaveType = _mapper.Map<LeaveType>(model);
        _context.Add(leaveType);
        await _context.SaveChangesAsync();
    }
    public bool LeaveTypeExists(int id)
    {
        return _context.LeaveTypes.Any(e => e.Id == id);
    }
    public async Task<bool> CheckIfLeaveTypeNameExists(string name)
    {
        var lowerCaseName = name.ToLower();
        return await _context.LeaveTypes.AnyAsync(q => q.Name.ToLower().Equals(lowerCaseName));
    }
    public async Task<bool> CheckIfLeaveTypeNameExistsForEdit(LeaveTypeEditVM leaveTypeEdit)
    {
        var lowerCaseName = leaveTypeEdit.Name.ToLower();
        return await _context.LeaveTypes.AnyAsync(q => q.Name.ToLower().Equals(lowerCaseName) && q.Id != leaveTypeEdit.Id);
    }

}
