using BaseLibrary.Responses;
using CarServiceApp.DTO;
using CarServiceApp.Entities;

namespace CarServiceApp.Services.Contracts
{
    public interface IServiceService
    {

        Task<GeneralResponse> CreateAsync(ServiceDTO serviceDto);
        Task<GeneralResponse> UpdateAsync(uint id, ServiceDTO serviceDto);
        Task<Service> GetByIdAsync(uint id);
        Task<GeneralResponse> DeleteAsync(uint id);
        Task<List<Service>> GetAllAsync();
    }
}
