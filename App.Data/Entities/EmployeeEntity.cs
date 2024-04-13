namespace App.Data.Entities;

using App.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public partial class EmployeeEntity
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public int EmployeeAge { get; set; }
    public EmploymentType EmployeeType { get; set; }

    public class Configuration : IEntityTypeConfiguration<EmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            builder.ToTable("employees", AppDbContextConstants.Schemas.Organization);
            builder.HasKey(e => e.EmployeeId);
            
            //The below is done for demonstration purpose. This should not be done in actual scenario.
            //Data should be sourced from a source once the db is up or tooling might be use to dump data.
            builder.HasData(new EmployeeEntity[]
            {
                new EmployeeEntity()
                {
                    EmployeeId = 1,
                    EmployeeAge = 30,
                    EmployeeName = "Test User",
                    EmployeeType = EmploymentType.Permanent
                },
                new EmployeeEntity()
                {
                    EmployeeId = 2,
                    EmployeeAge = 20,
                    EmployeeName = "Test User2",
                    EmployeeType = EmploymentType.Temporary
                }
            });
        }
    }
}
