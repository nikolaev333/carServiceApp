using AutoMapper;
using BaseLibrary.Responses;
using CarServiceApp.Data;
using CarServiceApp.DTO;
using CarServiceApp.Entities;
using CarServiceApp.Services.Contracts;
using Microsoft.EntityFrameworkCore;


namespace CarServiceApp.Services.Implementations
{
    public class ServiceService : IServiceService
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public ServiceService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GeneralResponse> CreateAsync(ServiceDTO serviceDto)
        {
            if (serviceDto == null)
            {
                return new GeneralResponse(false, "Invalid service data provided.");
            }

            var service = _mapper.Map<Service>(serviceDto);

            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Service successfully created.");
        }

        public async Task<GeneralResponse> UpdateAsync(uint id, ServiceDTO serviceDto)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return new GeneralResponse(false, "Service not found.");
            }

            service.CarId = serviceDto.CarId;
            service.DateReceived = serviceDto.DateReceived;
            service.Description = serviceDto.Description;
            service.Status = serviceDto.Status;

            _context.Services.Update(service);
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Service successfully updated.");
        }

        public async Task<Service> GetByIdAsync(uint id)
        {
            // Включване на свързаната кола и събитията, ако има такива
            var service = await _context.Services
        .Include(s => s.Car) // This will include the Car entity in the result
        .Include(s => s.ServiceEvents) // This will include the ServiceEvents collection in the result
        .FirstOrDefaultAsync(s => s.Id == id);

            return service;
        }


        public async Task<GeneralResponse> DeleteAsync(uint id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return new GeneralResponse(false, "Service not found.");
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Service successfully deleted.");
        }

        public async Task<List<Service>> GetAllAsync()
        {
            var services = await _context.Services.ToListAsync();
            

            return services;
        }
    }
}