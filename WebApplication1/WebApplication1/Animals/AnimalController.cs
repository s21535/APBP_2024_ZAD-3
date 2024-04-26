using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Animals;

[ApiController]
[Route("/api/animal")]
public class AnimalController : ControllerBase
{
    private readonly IAnimalService _animalService;
    public AnimalController(IAnimalService animalService)
    {
        _animalService = animalService;
    }
    
    [HttpGet("")]
    public IActionResult GetAllAnimals([FromQuery] string orderBy)
    {
        var animals = _animalService.GetAllAnimals(orderBy);
        return Ok(animals);
    }

    [HttpPost]
    public IActionResult CreateAnimal([FromBody] CreateAnimalDTO dto)
    {
        var success = _animalService.AddAnimal(dto);
        return success ? Created() : Conflict();
    }

    [HttpDelete]
    public IActionResult DeleteAnimal([FromBody] DeleteAnimalDTO dto)
    {
        var success = _animalService.DeleteAnimal(dto);
        return success ? Accepted() : Conflict();
    }
    
}