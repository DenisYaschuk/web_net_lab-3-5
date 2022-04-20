using BBL.DTO;
using BBL.Extensions;
using BBL.Interfaces;
using EF_DAL.Model;
using EF_DAL.Repositories;


namespace BBL.Services
{
    public class GrindstoneService : IGrindstoneService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GrindstoneService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GrindstoneDTO> CreateAsync(GrindstoneDTO grindstoneData)
        {

            var grindstone = new Grindstone
            {
                Id = grindstoneData.Id,
                Description = grindstoneData.Description,
                Priority = grindstoneData.Priority,
                StatusId = grindstoneData.StatusId,
                TimeToFinish = grindstoneData.TimeToFinish
            };

            await _unitOfWork.Grindstone.CreateAsync(grindstone);

            return grindstone?.ToDto();
        }
        public async Task<bool> UpdateAsync(GrindstoneDTO grindstone)
        {
            var storedGrindstone = await _unitOfWork.Grindstone.GetByIdAsync(grindstone.Id);
            if (storedGrindstone == null)
            {
                return false;
            }
            storedGrindstone.Update(grindstone);
            return await _unitOfWork.Grindstone.UpdateAsync(storedGrindstone);
        }

        public async Task<GrindstoneDTO> GetByIdAsync(int grindstoneId)
        {
            var grindstone = await _unitOfWork
                .Grindstone
                .GetByIdAsync(grindstoneId);
            return grindstone?.ToDto();
        }

        public async Task<IEnumerable<GrindstoneDTO>> GetAllAsync()
        {
            var grindstones = await _unitOfWork.Grindstone.GetAllAsync();
            var result = grindstones.Select(x => x?.ToDto());
            return result;
        }
        public async Task<bool> DeleteByIdAsync(int grindstoneId)
        {
            var grindstone = await _unitOfWork.Grindstone.GetByIdAsync(grindstoneId);

            if (grindstone == null)
            {
                return false;
            }

            await _unitOfWork.Grindstone.DeleteByIdAsync(grindstoneId);
            return true;
        }

    }
}
