using BBL.DTO;
using EF_DAL.Model;

namespace BBL.Extensions
{
    public static class GrindstoneExtensions
    {
        public static GrindstoneDTO ToDto(this Grindstone grindstone)
        {
            return new GrindstoneDTO()
            {
                Id = grindstone.Id,
                Description = grindstone.Description,
                TimeToFinish = grindstone.TimeToFinish,
                Priority = grindstone.Priority,
                StatusId = grindstone.StatusId
            };
        }

        public static void Update(this Grindstone grindstone, GrindstoneDTO dto)
        {
            grindstone.Description = dto.Description;
            grindstone.TimeToFinish = dto.TimeToFinish;
            grindstone.Priority = dto.Priority;
            grindstone.StatusId = dto.StatusId;
        }
    }
}

