using BaseLibrary.Responses;
using CarServiceApp.DTO;
using CarServiceApp.Entities;

namespace CarServiceApp.Services.Contracts
{
    public interface ICarService
    {
        Task<GeneralResponse> CreateAsync(CarDTO carDto);
        Task<GeneralResponse> UpdateAsync(uint id, CarDTO carDto);
        Task<Car> GetByIdAsync(uint id);
        Task<GeneralResponse> DeleteAsync(uint id);
        Task<List<Car>> GetAllAsync();
    }
}
