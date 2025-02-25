using Backend.MinimalApi.DTOs;

namespace Backend.MinimalApi.Endpoints;

public static class AnimalEndPoints
{
    private static List<AnimalDto> _animals = [
        new() {Type = "Rabbit", Sound= "Awii"},
        new() {Type = "Frog", Sound = "Quack"},
        new(){Type = "Dog", Sound = "Woof"}
    ];
    
    public static IEndpointRouteBuilder MapAnimalEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/animals");
        
        group.MapGet("", GetAll);
        group.MapGet("{type}", GetByType);
        
        group.MapPost("", AddAnimal);
        
        group.MapPut("{type}", UpdateAnimal);
        
        group.MapDelete("{type}", RemoveAnimal);
        
       
        return app;
    }

    

    private static IResult GetAll()
    {
        return Results.Ok(_animals);
    }

    private static IResult GetByType(string type)
    {
        var animal = _animals.FirstOrDefault(a => a.Type == type);

        return animal is null ? Results.NotFound() : Results.Ok(animal);
    }
    private static IResult AddAnimal(AnimalDto animal)
    {
        _animals.Add(animal);

        return Results.Created<AnimalDto>($"api/animals/{animal.Type}", animal);
    }

    private static IResult UpdateAnimal(string type, AnimalDto animalDto)
    {
        var animal = _animals.FirstOrDefault(a => a.Type == type);

        if (animal is null) return Results.NotFound();

        animal.Type = animalDto.Type;
        animal.Sound = animalDto.Sound;

        return Results.NoContent();
    }

    private static IResult RemoveAnimal(string type)
    {
        if (_animals.FirstOrDefault(a => a.Type == type) is not AnimalDto animalDto) return Results.NotFound();
        
        _animals.Remove(animalDto);
        return Results.NoContent();

    }
}