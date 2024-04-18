using CarServiceApp.DTO;
using CarServiceApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarServiceApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateService(ServiceDTO serviceDto)
        {
            var response = await _serviceService.CreateAsync(serviceDto);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(uint id, ServiceDTO serviceDto)
        {
            var response = await _serviceService.UpdateAsync(id, serviceDto);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(uint id)
        {
            var serviceDto = await _serviceService.GetByIdAsync(id);
            if (serviceDto == null)
            {
                return NotFound("Service not found");
            }

            return Ok(serviceDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(uint id)
        {
            var response = await _serviceService.DeleteAsync(id);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            var serviceDtos = await _serviceService.GetAllAsync();
            return Ok(serviceDtos);
        }
    }
}