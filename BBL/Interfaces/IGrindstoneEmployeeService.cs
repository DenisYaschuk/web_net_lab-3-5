using BBL.DTO;

namespace BBL.Interfaces
{
    public interface IGrindstoneEmployeeService
    {
        public Task<GrindstoneEmployeeDTO> CreateAsync(GrindstoneEmployeeDTO GrindstoneEmployeeData);
        public Task<bool> UpdateAsync(GrindstoneEmployeeDTO grindstone);
        public Task<GrindstoneEmployeeDTO> GetByIdAsync(int Id);
        public Task<IEnumerable<GrindstoneEmployeeDTO>> GetAllAsync();
        public Task<bool> DeleteByIdAsync(int Id);
    }
}