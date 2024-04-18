using BaseLibrary.Responses;
using CarServiceApp.Data;
using CarServiceApp.DTO;
using CarServiceApp.Entities;
using CarServiceApp.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarServiceApp.Services.Implementations
{
    public class CarService : ICarService
    {
        private readonly AppDbContext _context;

        public CarService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> CreateAsync(CarDTO carDto)
        {
            if (carDto == null)
            {
                return new GeneralResponse(false, "Invalid car data provided.", null);
            }

            var car = new Car
            {
                OwnerUserId = carDto.OwnerUserId,
                Make = carDto.Make,
                Model = carDto.Model,
                Year = carDto.Year,
                LicensePlate = carDto.LicensePlate
            };

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Car successfully created.", null);
        }

        public async Task<GeneralResponse> UpdateAsync(uint id, CarDTO carDto)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return new GeneralResponse(false, "Car not found.", null);
            }

            car.OwnerUserId = carDto.OwnerUserId;
            car.Make = carDto.Make;
            car.Model = carDto.Model;
            car.Year = carDto.Year;
            car.LicensePlate = carDto.LicensePlate;

            _context.Cars.Update(car);
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Car successfully updated.", null);
        }

        public async Task<CarDTO> GetByIdAsync(uint id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return null;
            }

            var carDto = new CarDTO
            {
  
                OwnerUserId = car.OwnerUserId,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                LicensePlate = car.LicensePlate
            };

            return carDto;
        }

        public async Task<GeneralResponse> DeleteAsync(uint id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return new GeneralResponse(false, "Car not found.", null);
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Car successfully deleted.", null);
        }

        public async Task<List<CarDTO>> GetAllAsync()
        {
            var cars = await _context.Cars.ToListAsync();
            var carDtos = new List<CarDTO>();

            foreach (var car in cars)
            {
                var carDto = new CarDTO
                {
       
                    OwnerUserId = car.OwnerUserId,
                    Make = car.Make,
                    Model = car.Model,
                    Year = car.Year,
                    LicensePlate = car.LicensePlate
                };
                carDtos.Add(carDto);
            }

            return carDtos;
        }
    }
}
