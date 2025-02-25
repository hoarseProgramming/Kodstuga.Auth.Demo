using Backend.MinimalApi.Dtos;
using Backend.MinimalApi.Interfaces;

namespace Backend.MinimalApi.Services;

public class AnimalService : IAnimalService
{
    private readonly List<AnimalDto> _animals;

    public AnimalService()
    {
        _animals = new List<AnimalDto>
        {
            new AnimalDto(1, "Cat", "Garfield", "Meeeaaow"),
            new AnimalDto(2, "Dog", "Scooby", "Scobydoby do!"),
            new AnimalDto(3, "Duck", "Duckey", "Quack quack"),
        };
    }

    public async Task<IEnumerable<AnimalDto>> GetAllAnimalsAsync()
    {
        return _animals.OrderBy(x => x.Id);
    }

    public async Task<AnimalDto?> GetAnimalByIdAsync(int id)
    {
        return _animals.FirstOrDefault(x => x.Id == id);
    }

    public async Task<AnimalDto> PostAnimalAsync(AnimalDto animal)
    {
        var newId = _animals.MaxBy(x => x.Id)?.Id;

        if (newId is null)
        {
            newId = 0;
        }
        newId++;

        var newAnimal = new AnimalDto((int)newId!, animal.Type, animal.Name, animal.Sound);

        _animals.Add(newAnimal);
        return newAnimal;
    }

    public async Task<AnimalDto?> UpdateAnimalAsync(AnimalDto animal)
    {
        var animalFromList = _animals.FirstOrDefault(x => x.Id == animal.Id);

        if (animalFromList is null)
        {
            return null;
        }

        _animals.Remove(animalFromList);
        _animals.Add(animal);
        return animal;
    }

    public async Task<bool> DeleteAnimalAsync(int id)
    {
        var animalFromList = _animals.FirstOrDefault(x => x.Id == id);

        if (animalFromList is null)
        {
            return false;
        }

        _animals.Remove(animalFromList);
        return true;
    }
}