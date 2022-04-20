
using BBL.DTO;
using BBL.Extensions;
using BBL.Interfaces;
using EF_DAL.Model;
using EF_DAL.Repositories;


namespace BBL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployeeDTO> CreateAsync(EmployeeDTO employeeData)
        {

            var employee = new Employee
            {
                Id = employeeData.Id,
                FirstName = employeeData.FirstName,
                LastName = employeeData.LastName,
                TeamId = employeeData.TeamId

            };

            await _unitOfWork.Employee.CreateAsync(employee);

            return employee?.ToDto();
        }
        public async Task<bool> UpdateAsync(EmployeeDTO employee)
        {
            var storedEmployee = await _unitOfWork.Employee.GetByIdAsync(employee.Id);
            if (storedEmployee == null)
            {
                return false;
            }
            storedEmployee.Update(employee);
            return await _unitOfWork.Employee.UpdateAsync(storedEmployee);
        }

        public async Task<EmployeeDTO> GetByIdAsync(int employeeId)
        {
            var employee = await _unitOfWork
                .Employee
                .GetByIdAsync(employeeId);
            return employee?.ToDto();
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllAsync()
        {
            var employees = await _unitOfWork.Employee.GetAllAsync();
            var result = employees.Select(x => x?.ToDto());
            return result;
        }
        public async Task<bool> DeleteByIdAsync(int employeeId)
        {
            var employee = await _unitOfWork.Employee.GetByIdAsync(employeeId);

            if (employee == null)
            {
                return false;
            }

            await _unitOfWork.Employee.DeleteByIdAsync(employeeId);
            return true;
        }

    }
}
