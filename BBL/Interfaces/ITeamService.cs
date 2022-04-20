using BBL.DTO;

namespace BBL.Interfaces
{
    public interface ITeamService
    {
        public Task<TeamDTO> CreateAsync(TeamDTO teamData);
        public Task<bool> UpdateAsync(TeamDTO team);
        public Task<TeamDTO> GetByIdAsync(int Id);
        public Task<IEnumerable<TeamDTO>> GetAllAsync();
        public Task<bool> DeleteByIdAsync(int Id);
    }
}
