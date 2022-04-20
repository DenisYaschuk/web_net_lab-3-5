
using BBL.DTO;

namespace BBL.Interfaces
{
    public interface IEmployeeService
    {
        public Task<EmployeeDTO> CreateAsync(EmployeeDTO employeeData);
        public Task<bool> UpdateAsync(EmployeeDTO employee);
        public Task<EmployeeDTO> GetByIdAsync(int Id);
        public Task<IEnumerable<EmployeeDTO>> GetAllAsync();
        public Task<bool> DeleteByIdAsync(int Id);
    }
}