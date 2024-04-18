using BaseLibrary.Responses;
using CarServiceApp.DTO;

namespace CarServiceApp.Services.Contracts
{
    public interface ICarService
    {
        Task<GeneralResponse> CreateAsync(CarDTO carDto);
        Task<GeneralResponse> UpdateAsync(uint id, CarDTO carDto);
        Task<CarDTO> GetByIdAsync(uint id);
        Task<GeneralResponse> DeleteAsync(uint id);
        Task<List<CarDTO>> GetAllAsync();
    }
}
