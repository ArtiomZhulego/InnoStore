using Application.Abstractions.DTOs.Clients.HRM.Employees;
using Domain.Entities;

namespace Application.Mappers;

public static class UserMappers
{
    public static IEnumerable<User> ToUsers(this IEnumerable<EmployeeModel> employeeModels)
    {
        return employeeModels.Select(employeeModel => new User
        {
            HrmId = employeeModel.Id,
            FirstNameRU = employeeModel.FirstNameRU,
            Birthdate = employeeModel.Birthdate,
            PatronymicRU = employeeModel.PatronymicRU,
            LastNameRU = employeeModel.LastNameRU,
            FirstNameEN = employeeModel.FirstNameEN,
            PatronymicEN = employeeModel.PatronymicEN,
            LastNameEN = employeeModel.LastNameEN,
            Email = employeeModel.Email,
            OfficeId = employeeModel.OfficeId,
            JobTitleId = employeeModel.JobTitleId,
            LinkProfilePictureMini = employeeModel.LinkProfilePictureMini,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });
    }
}