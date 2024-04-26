using System.Data.SqlClient;

namespace WebApplication1.Animals;

public interface IAnimalRepository
{
    public IEnumerable<Animal> FetchAllAnimals(string orderBy);
    public bool CreateAnimal(string Name);
}

public class AnimalRepository : IAnimalRepository
{
    private readonly IConfiguration _configuration;

    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Animal> FetchAllAnimals(string orderBy)
    {
        var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand($"SELECT IdAnimal, Name, Description, CATEGORY, AREA FROM Animals ORDER BY @orderBy", connection);
        command.Parameters.AddWithValue("orderBy", orderBy == "" ? "Name" : orderBy);
        using var reader = command.ExecuteReader();

        var animals = new List<Animal>();
        while (reader.Read())
        {
            var animal = new Animal()
            {
                IdAnimal = (int)reader["IdAnimal"],
                Name = reader["Name"].ToString()!,
                Description = reader["Description"].ToString()!,
                Category = reader["CATEGORY"].ToString()!,
                Area = reader["AREA"].ToString()!
            };
            animals.Add(animal);
        }

        return animals;
    }
    
    public bool CreateAnimal(string Name)
    {
        var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand("INSERT INTO Animals VALUES (@Name)", connection);
        command.Parameters.AddWithValue("Name", Name);
        var affectedRows = command.ExecuteNonQuery();
        return affectedRows == 1;
    }
    
}