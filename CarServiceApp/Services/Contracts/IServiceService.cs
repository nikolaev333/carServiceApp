using BaseLibrary.Responses;
using CarServiceApp.DTO;

namespace CarServiceApp.Services.Contracts
{
    public interface IServiceService
    {

        Task<GeneralResponse> CreateAsync(ServiceDTO serviceDto);
        Task<GeneralResponse> UpdateAsync(uint id, ServiceDTO serviceDto);
        Task<ServiceDTO> GetByIdAsync(uint id);
        Task<GeneralResponse> DeleteAsync(uint id);
        Task<List<ServiceDTO>> GetAllAsync();
    }
}
