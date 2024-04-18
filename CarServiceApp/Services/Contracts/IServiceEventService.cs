using BaseLibrary.Responses;
using CarServiceApp.DTO;

namespace CarServiceApp.Services.Contracts
{
    public interface IServiceEventService
    {
        Task<GeneralResponse> CreateAsync(ServiceEventDTO serviceEventDto);
        Task<GeneralResponse> UpdateAsync(uint id, ServiceEventDTO serviceEventDto);
        Task<ServiceEventDTO> GetByIdAsync(uint id);
        Task<GeneralResponse> DeleteAsync(uint id);
        Task<List<ServiceEventDTO>> GetAllAsync();
    }
}
