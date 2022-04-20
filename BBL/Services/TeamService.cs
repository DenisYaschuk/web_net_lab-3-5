using BBL.DTO;
using BBL.Extensions;
using BBL.Interfaces;
using EF_DAL.Model;
using EF_DAL.Repositories;


namespace BBL.Services
{
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TeamDTO> CreateAsync(TeamDTO teamData)
        {

            var team = new Team
            {
                Id = teamData.Id,
                Name = teamData.Name
            };

            await _unitOfWork.Team.CreateAsync(team);

            return team?.ToDto();
        }
        public async Task<bool> UpdateAsync(TeamDTO team)
        {
            var storedTeam = await _unitOfWork.Team.GetByIdAsync(team.Id);
            if (storedTeam == null)
            {
                return false;
            }
            storedTeam.Update(team);
            return await _unitOfWork.Team.UpdateAsync(storedTeam);
        }

        public async Task<TeamDTO> GetByIdAsync(int teamId)
        {
            var team = await _unitOfWork
                .Team
                .GetByIdAsync(teamId);
            return team?.ToDto();
        }

        public async Task<IEnumerable<TeamDTO>> GetAllAsync()
        {
            var teams = await _unitOfWork.Team.GetAllAsync();
            var result = teams.Select(x => x.ToDto());
            return result;
        }
        public async Task<bool> DeleteByIdAsync(int teamId)
        {
            var team = await _unitOfWork.Team.GetByIdAsync(teamId);

            if (team == null)
            {
                return false;
            }

            await _unitOfWork.Team.DeleteByIdAsync(teamId);
            return true;
        }

    }
}
