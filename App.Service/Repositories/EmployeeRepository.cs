namespace App.Services.Repositories;

using App.Data;
using App.Data.Entities;
using Microsoft.EntityFrameworkCore;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _dbContext;

    public EmployeeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<EmployeeEntity?> GetById(int id)
    {
        var employee = await _dbContext.Employees
            .Where(e => e.EmployeeId == id)
            .FirstOrDefaultAsync();

        return employee;
    }
}
