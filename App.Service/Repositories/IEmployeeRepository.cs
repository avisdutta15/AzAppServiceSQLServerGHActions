namespace App.Services.Repositories;

using App.Data.Entities;

public interface IEmployeeRepository
{
    Task<EmployeeEntity?> GetById(int id);
}
