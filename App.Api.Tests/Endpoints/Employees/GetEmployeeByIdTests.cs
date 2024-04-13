namespace App.Api.Tests.Endpoints.Employees;

using App.Api.Endpoints.Employees;
using App.Data;
using App.Data.Entities;
using App.Services.Repositories;
using App.TestingToolbox;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using static App.TestingToolbox.DomainEntityFakers;

public class GetEmployeeByIdTests : WithAppContextContainer
{
    [Fact]
    public async Task InsertEmployee_RetunsInsertedEmployee()
    {
        //Arrange
        var employees = new EmployeeEntityFaker().Generate(2);
        AppDbContext.Employees.AddRange(employees);

        await AppDbContext.SaveChangesAsync(CancellationToken.None);

        var employeeRepository = Substitute.For<IEmployeeRepository>();
        var employee = employees.First();
        employeeRepository.GetById(employee.EmployeeId).Returns(employee);

        //Act
        var response = await GetEmployeeById.Handle(employee.EmployeeId, employeeRepository);

        //Assert
        var result = response.Result as Ok<EmployeeEntity>;
        result!.Should().NotBeNull();
        result!.Value!.Should().BeEquivalentTo(employee);
    }

    [Fact]
    public async Task EmptyDatabase_RetunsEmployeeNotFound()
    {
        //Arrange
        var employeeRepository = Substitute.For<IEmployeeRepository>();
        var employeeId = 10;

        //Act
        var response = await GetEmployeeById.Handle(employeeId, employeeRepository);

        // Assert
        response.Result.Should().BeOfType<NotFound>();
    }
}