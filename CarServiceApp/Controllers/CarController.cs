using CarServiceApp.DTO;
using CarServiceApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarServiceApp.Controllers
{
    [ApiController]
    [Route("car")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(CarDTO carDto)
        {
            var response = await _carService.CreateAsync(carDto);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(uint id, CarDTO carDto)
        {
            var response = await _carService.UpdateAsync(id, carDto);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(uint id)
        {
            var carDto = await _carService.GetByIdAsync(id);
            if (carDto == null)
            {
                return NotFound("Car not found");
            }

            return Ok(carDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(uint id)
        {
            var response = await _carService.DeleteAsync(id);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            var carDtos = await _carService.GetAllAsync();
            return Ok(carDtos);
        }
    }
}
