using AutoMapper;
using BaseLibrary.Responses;
using CarServiceApp.Data;
using CarServiceApp.DTO;
using CarServiceApp.Entities;
using CarServiceApp.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace CarServiceApp.Services.Implementations
{
    public class CarService : ICarService
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public CarService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GeneralResponse> CreateAsync(CarDTO carDto)
        {
            var validationResponse = await ValidateCarDataAsync(carDto);
            if (validationResponse != null)
            {
                return validationResponse;
            }

            var car = _mapper.Map<Car>(carDto);

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Car successfully created.");
        }

        public async Task<GeneralResponse> UpdateAsync(uint id, CarDTO carDto)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return new GeneralResponse(false, "Car not found.");
            }

            var validationResponse = await ValidateCarDataAsync(carDto);
            if (validationResponse != null)
            {
                return validationResponse;
            }

            car.OwnerUserId = carDto.OwnerUserId;
            car.Make = carDto.Make;
            car.Model = carDto.Model;
            car.Year = carDto.Year;
            car.LicensePlate = carDto.LicensePlate;

            _context.Cars.Update(car);
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Car successfully updated.");
        }

        public async Task<Car> GetByIdAsync(uint id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return null;
            }

            return await _context.Cars.FindAsync(id);
        }

        public async Task<GeneralResponse> DeleteAsync(uint id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return new GeneralResponse(false, "Car not found.");
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Car successfully deleted.");
        }

        public async Task<List<Car>> GetAllAsync()
        {
            var cars = await _context.Cars.ToListAsync();
            var carDtos = new List<CarDTO>();

            return cars;
        }

        private async Task<GeneralResponse> ValidateCarDataAsync(CarDTO carDto)
        {
            if (carDto == null)
            {
                return new GeneralResponse(false, "Invalid car data provided.");
            }

            var ownerExists = await _context.Users.AnyAsync(u => u.Id == carDto.OwnerUserId);

            if (!ownerExists)
            {
                return new GeneralResponse(false, $"Owner with ID {carDto.OwnerUserId} does not exist.");
            }

            var existingLicensePlate = await _context.Cars.FirstOrDefaultAsync(c => c.LicensePlate == carDto.LicensePlate);

            if (existingLicensePlate is not null)
            {
                return new GeneralResponse(false, $"Car with license plate {carDto.LicensePlate} already exists.");
            }

            bool isValid = Regex.IsMatch(carDto.LicensePlate, @"^[A-Z]{1,2}\d{4}[A-]{2,3}$");

            if (!isValid)
            {
                return new GeneralResponse(false, "Invalid license plate");
            }

            return null; 
        }
    }
}
