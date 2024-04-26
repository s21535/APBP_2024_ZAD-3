using Microsoft.AspNetCore.Mvc;
using WebApplication1.Animals;

namespace WebApplication1.Controller;

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
}