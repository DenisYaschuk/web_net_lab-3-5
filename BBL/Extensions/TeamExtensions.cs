using BBL.DTO;
using EF_DAL.Model;

namespace BBL.Extensions
{
    public static class TeamExtensions
    {
        public static TeamDTO ToDto(this Team team)
        {
            return new TeamDTO()
            {
                Id = team.Id,
                Name = team.Name
            };
        }

        public static void Update(this Team team, TeamDTO dto)
        {
            team.Name = dto.Name;
        }
    }
}
