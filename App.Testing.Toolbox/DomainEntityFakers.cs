namespace App.TestingToolbox;

using App.Data.Entities;
using App.Data.Enums;
using AutoBogus;

public static class DomainEntityFakers
{
    public sealed class EmployeeEntityFaker : AutoFaker<EmployeeEntity>
    {
        public EmployeeEntityFaker()
        {
            StrictMode(true)
                .RuleFor(e => e.EmployeeId, f => f.Random.Int(100,200))
                .RuleFor(e => e.EmployeeAge, f => f.Random.Int(100, 200))
                .RuleFor(e => e.EmployeeName, f => f.Person.FirstName.ToString())
                .RuleFor(e => e.EmployeeType, f => f.PickRandom<EmploymentType>());
        }
    }
}
