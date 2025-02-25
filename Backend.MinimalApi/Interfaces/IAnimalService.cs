using Backend.MinimalApi.Dtos;

namespace Backend.MinimalApi.Interfaces;

public interface IAnimalService
{
    Task<IEnumerable<AnimalDto>> GetAllAnimalsAsync();
    Task<AnimalDto?> GetAnimalByIdAsync(int id);
    Task<AnimalDto> PostAnimalAsync(AnimalDto animal);
    Task<AnimalDto?> UpdateAnimalAsync(AnimalDto animal);
    Task<bool> DeleteAnimalAsync(int id);
}