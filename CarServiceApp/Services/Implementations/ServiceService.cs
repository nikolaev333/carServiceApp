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

        public ServiceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> CreateAsync(ServiceDTO serviceDto)
        {
            if (serviceDto == null)
            {
                return new GeneralResponse(false, "Invalid service data provided.", null);
            }

            var service = new Service
            {
                CarId = serviceDto.CarId,
                DateReceived = serviceDto.DateReceived,
                Description = serviceDto.Description,
                Status = serviceDto.Status
            };

            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Service successfully created.", null);
        }

        public async Task<GeneralResponse> UpdateAsync(uint id, ServiceDTO serviceDto)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return new GeneralResponse(false, "Service not found.", null);
            }

            service.CarId = serviceDto.CarId;
            service.DateReceived = serviceDto.DateReceived;
            service.Description = serviceDto.Description;
            service.Status = serviceDto.Status;

            _context.Services.Update(service);
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Service successfully updated.", null);
        }

        public async Task<ServiceDTO> GetByIdAsync(uint id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return null;
            }

            var serviceDto = new ServiceDTO
            {
 
                CarId = service.CarId,
                DateReceived = service.DateReceived,
                Description = service.Description,
                Status = service.Status
            };

            return serviceDto;
        }

        public async Task<GeneralResponse> DeleteAsync(uint id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return new GeneralResponse(false, "Service not found.", null);
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Service successfully deleted.", null);
        }

        public async Task<List<ServiceDTO>> GetAllAsync()
        {
            var services = await _context.Services.ToListAsync();
            var serviceDtos = new List<ServiceDTO>();

            foreach (var service in services)
            {
                var serviceDto = new ServiceDTO
                {
                 
                    CarId = service.CarId,
                    DateReceived = service.DateReceived,
                    Description = service.Description,
                    Status = service.Status
                };
                serviceDtos.Add(serviceDto);
            }

            return serviceDtos;
        }
    }
}