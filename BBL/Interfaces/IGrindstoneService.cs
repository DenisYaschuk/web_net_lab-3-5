using BBL.DTO;

namespace BBL.Interfaces
{
    public interface IGrindstoneService
    {
        public Task<GrindstoneDTO> CreateAsync(GrindstoneDTO grindstoneData);
        public Task<bool> UpdateAsync(GrindstoneDTO grindstone);
        public Task<GrindstoneDTO> GetByIdAsync(int Id);
        public Task<IEnumerable<GrindstoneDTO>> GetAllAsync();
        public Task<bool> DeleteByIdAsync(int Id);
    }
}