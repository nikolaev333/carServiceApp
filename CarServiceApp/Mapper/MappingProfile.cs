using AutoMapper;
using CarServiceApp.DTO;
using CarServiceApp.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Add as many of these lines as you need to map your objects
        CreateMap<CarDTO, Car>();
        CreateMap<ServiceEventDTO, ServiceEvent>();
    }
}