using BBL.DTO;
using EF_DAL.Model;

namespace BBL.Extensions
{
    public static class StatusExtensions
    {
        public static StatusDTO ToDto(this Status status)
        {
            return new StatusDTO()
            {
                Id = status.Id,
                Name = status.Name
            };
        }

        public static void Update(this Status status, StatusDTO dto)
        {
            status.Name = dto.Name;
        }
    }
}

