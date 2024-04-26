namespace WebApplication1.Animals;

public interface IAnimalService
{
    IEnumerable<Animal> GetAllAnimals(string orderBy);
    public bool AddAnimal(CreateAnimalDTO dto);
    public bool DeleteAnimal(DeleteAnimalDTO dto);
}

public class AnimalService : IAnimalService
{

    private readonly IAnimalRepository _animalRepository;
    public AnimalService(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }
    
    public IEnumerable<Animal> GetAllAnimals(string orderBy)
    {
        return _animalRepository.FetchAllAnimals(orderBy);
    }

    public bool AddAnimal(CreateAnimalDTO dto)
    {
        return _animalRepository.CreateAnimal(dto.Name);
    }

    public bool DeleteAnimal(DeleteAnimalDTO dto)
    {
        return _animalRepository.DeleteAnimal(dto.IdAnimal);
    }
    
}