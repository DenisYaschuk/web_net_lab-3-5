using BBL.DTO;
using BBL.Extensions;
using BBL.Interfaces;
using EF_DAL.Model;
using EF_DAL.Repositories;


namespace BBL.Services
{
    public class GrindstoneEmployeeService : IGrindstoneEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GrindstoneEmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GrindstoneEmployeeDTO> CreateAsync(GrindstoneEmployeeDTO GrindstoneEmployeeData)
        {

            var asignment = new GrindstoneEmployee
            {
                Id = GrindstoneEmployeeData.Id,
                GrindstoneId = GrindstoneEmployeeData.GrindstoneId,
                EmployeeId = GrindstoneEmployeeData.EmployeeId
            };

            await _unitOfWork.GrindstoneEmployee.CreateAsync(asignment);

            return asignment?.ToDto();
        }
        public async Task<bool> UpdateAsync(GrindstoneEmployeeDTO asignment)
        {
            var storedAsignment = await _unitOfWork.GrindstoneEmployee.GetByIdAsync(asignment.Id);
            if (storedAsignment == null)
            {
                return false;
            }
            storedAsignment.Update(asignment);
            return await _unitOfWork.GrindstoneEmployee.UpdateAsync(storedAsignment);
        }

        public async Task<GrindstoneEmployeeDTO> GetByIdAsync(int asignmentId)
        {
            var asignment = await _unitOfWork
                .GrindstoneEmployee
                .GetByIdAsync(asignmentId);
            return asignment?.ToDto();
        }

        public async Task<IEnumerable<GrindstoneEmployeeDTO>> GetAllAsync()
        {
            var asignments = await _unitOfWork.GrindstoneEmployee.GetAllAsync();
            var result = asignments.Select(x => x?.ToDto());
            return result;
        }
        public async Task<bool> DeleteByIdAsync(int asignmentId)
        {
            var asignment = await _unitOfWork.GrindstoneEmployee.GetByIdAsync(asignmentId);

            if (asignment == null)
            {
                return false;
            }

            await _unitOfWork.GrindstoneEmployee.DeleteByIdAsync(asignmentId);
            return true;
        }

    }
}
