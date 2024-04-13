namespace App.Api.Endpoints.Employees;

using App.Data.Entities;
using App.Services.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public class GetEmployeeById
{
    public static async Task<Results<Ok<EmployeeEntity>, NotFound>> Handle(
        [FromRoute] int empId,
        IEmployeeRepository employeeRepository)
    {
        var employee = await employeeRepository.GetById(empId);

        if (employee == null)
            return TypedResults.NotFound();

        return TypedResults.Ok(employee);
    }
}
