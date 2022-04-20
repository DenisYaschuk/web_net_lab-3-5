using BBL.DTO;
using EF_DAL.Model;

namespace BBL.Extensions
{
    public static class EmployeeExtensions
    {
        public static EmployeeDTO ToDto(this Employee employee)
        {
            return new EmployeeDTO()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                TeamId = employee.TeamId,
            };
        }

        public static void Update(this Employee employee, EmployeeDTO dto)
        {
            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.TeamId = dto.TeamId;
        }
    }
}

