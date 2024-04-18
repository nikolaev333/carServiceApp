using BaseLibrary.Responses;
using CarServiceApp.Data;
using CarServiceApp.DTO;
using CarServiceApp.Entities;
using CarServiceApp.Services.Contracts;
using Microsoft.EntityFrameworkCore;


namespace CarServiceApp.Services.Implementations
{
    public class ServiceEventService : IServiceEventService
    {
        private readonly AppDbContext _context;

        public ServiceEventService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> CreateAsync(ServiceEventDTO serviceEventDto)
        {
            if (serviceEventDto == null)
            {
                return new GeneralResponse(false, "Invalid service event data provided.");
            }

            var serviceEvent = new ServiceEvent
            {
                ServiceId = serviceEventDto.ServiceId,
                EventDescription = serviceEventDto.EventDescription,
                EventDate = serviceEventDto.EventDate
            };

            await _context.ServiceEvents.AddAsync(serviceEvent);
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Service event successfully created.");
        }

        public async Task<GeneralResponse> UpdateAsync(uint id, ServiceEventDTO serviceEventDto)
        {
            var serviceEvent = await _context.ServiceEvents.FindAsync(id);
            if (serviceEvent == null)
            {
                return new GeneralResponse(false, "Service event not found.");
            }

            serviceEvent.ServiceId = serviceEventDto.ServiceId;
            serviceEvent.EventDescription = serviceEventDto.EventDescription;
            serviceEvent.EventDate = serviceEventDto.EventDate;

            _context.ServiceEvents.Update(serviceEvent);
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Service event successfully updated.");
        }

        public async Task<ServiceEventDTO> GetByIdAsync(uint id)
        {
            var serviceEvent = await _context.ServiceEvents.FindAsync(id);
            if (serviceEvent == null)
            {
                return null;
            }

            var serviceEventDto = new ServiceEventDTO
            {
               
                ServiceId = serviceEvent.ServiceId,
                EventDescription = serviceEvent.EventDescription,
                EventDate = serviceEvent.EventDate
            };

            return serviceEventDto;
        }

        public async Task<GeneralResponse> DeleteAsync(uint id)
        {
            var serviceEvent = await _context.ServiceEvents.FindAsync(id);
            if (serviceEvent == null)
            {
                return new GeneralResponse(false, "Service event not found.");
            }

            _context.ServiceEvents.Remove(serviceEvent);
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Service event successfully deleted.");
        }

        public async Task<List<ServiceEventDTO>> GetAllAsync()
        {
            var serviceEvents = await _context.ServiceEvents.ToListAsync();
            var serviceEventDtos = new List<ServiceEventDTO>();

            foreach (var serviceEvent in serviceEvents)
            {
                var serviceEventDto = new ServiceEventDTO
                {
                    ServiceId = serviceEvent.ServiceId,
                    EventDescription = serviceEvent.EventDescription,
                    EventDate = serviceEvent.EventDate
                };
                serviceEventDtos.Add(serviceEventDto);
            }

            return serviceEventDtos;
        }
    }
}