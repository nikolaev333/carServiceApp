using CarServiceApp.DTO;
using CarServiceApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarServiceApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceEventController : ControllerBase
    {
        private readonly IServiceEventService _serviceEventService;

        public ServiceEventController(IServiceEventService serviceEventService)
        {
            _serviceEventService = serviceEventService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateServiceEvent(ServiceEventDTO serviceEventDto)
        {
            var response = await _serviceEventService.CreateAsync(serviceEventDto);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceEvent(uint id, ServiceEventDTO serviceEventDto)
        {
            var response = await _serviceEventService.UpdateAsync(id, serviceEventDto);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceEventById(uint id)
        {
            var serviceEventDto = await _serviceEventService.GetByIdAsync(id);
            if (serviceEventDto == null)
            {
                return NotFound("Service event not found");
            }

            return Ok(serviceEventDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceEvent(uint id)
        {
            var response = await _serviceEventService.DeleteAsync(id);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServiceEvents()
        {
            var serviceEventDtos = await _serviceEventService.GetAllAsync();
            return Ok(serviceEventDtos);
        }
    }
}