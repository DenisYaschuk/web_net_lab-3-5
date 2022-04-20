using BBL.DTO;
using EF_DAL.Model;

namespace BBL.Extensions
{
    public static class GrindstoneEmployeeExtension
    {
        public static GrindstoneEmployeeDTO ToDto(this GrindstoneEmployee asignment)
        {
            return new GrindstoneEmployeeDTO()
            {
                Id = asignment.Id,
                GrindstoneId = asignment.GrindstoneId,
                EmployeeId = asignment.EmployeeId
            };
        }

        public static void Update(this GrindstoneEmployee asignment, GrindstoneEmployeeDTO dto)
        {
            asignment.GrindstoneId = dto.GrindstoneId;
            asignment.EmployeeId = dto.EmployeeId;
        }
    }
}


