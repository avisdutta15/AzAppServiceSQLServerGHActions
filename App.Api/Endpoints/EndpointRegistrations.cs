namespace App.Api.Endpoints;

using App.Api.Endpoints.Employees;

public static class EndpointRegistrations
{
    public static WebApplication UseCustomEndpoints(this WebApplication app)
    {
        UseEmployeesEndpoints(app);

        return app;
    }

    private static WebApplication UseEmployeesEndpoints(this WebApplication app)
    {
        var employeesGroup = app.MapGroup("/employees")
            .WithTags("Employees");

        employeesGroup.MapGet("/{empId}", GetEmployeeById.Handle);
        return app;
    }
}
