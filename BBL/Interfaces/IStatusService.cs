using BBL.DTO;

namespace BBL.Interfaces
{
    public interface IStatusService
    {
        public Task<StatusDTO> CreateAsync(StatusDTO statusData);
        public Task<bool> UpdateAsync(StatusDTO status);
        public Task<StatusDTO> GetByIdAsync(int Id);
        public Task<IEnumerable<StatusDTO>> GetAllAsync();
        public Task<bool> DeleteByIdAsync(int Id);
    }
}