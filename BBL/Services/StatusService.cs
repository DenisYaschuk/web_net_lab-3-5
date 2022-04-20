using BBL.DTO;
using BBL.Extensions;
using BBL.Interfaces;
using EF_DAL.Model;
using EF_DAL.Repositories;


namespace BBL.Services
{
    public class StatusService : IStatusService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<StatusDTO> CreateAsync(StatusDTO statusData)
        {

            var status = new Status
            {
                Id = statusData.Id,
                Name = statusData.Name
            };

            await _unitOfWork.Status.CreateAsync(status);

            return status?.ToDto();
        }
        public async Task<bool> UpdateAsync(StatusDTO status)
        {
            var storedStatus = await _unitOfWork.Status.GetByIdAsync(status.Id);
            if (storedStatus == null)
            {
                return false;
            }
            storedStatus.Update(status);
            return await _unitOfWork.Status.UpdateAsync(storedStatus);
        }

        public async Task<StatusDTO> GetByIdAsync(int statusId)
        {
            var status = await _unitOfWork
                .Status
                .GetByIdAsync(statusId);
            return status?.ToDto();
        }

        public async Task<IEnumerable<StatusDTO>> GetAllAsync()
        {
            var statuses = await _unitOfWork.Status.GetAllAsync();
            var result = statuses.Select(x => x?.ToDto());
            return result;
        }
        public async Task<bool> DeleteByIdAsync(int statusId)
        {
            var status = await _unitOfWork.Status.GetByIdAsync(statusId);

            if (status == null)
            {
                return false;
            }

            await _unitOfWork.Status.DeleteByIdAsync(statusId);
            return true;
        }

    }
}
