global using AutoMapper;
using DotnetSteps.Dtos.Character;
using DotnetSteps.Models;

namespace DotnetSteps.Services.CharacterService;

public class CharacterService : ICharacterService
{

    private static List<Character> characters = new List<Character>
    {
        new Character(),
        new Character{Id=1, Name="Samuel"}
};
    private readonly IMapper _mapper;

    public CharacterService(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
        var ServiceResponse = new ServiceResponse<List<GetCharacterDto>>();
        var character = _mapper.Map<Character>(newCharacter);
        character.Id = characters.Max(c => c.Id) + 1;
        characters.Add(_mapper.Map<Character>(character));
        ServiceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        return ServiceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
        var ServiceResponse = new ServiceResponse<List<GetCharacterDto>>();

        ServiceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        return ServiceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
        var ServiceResponse = new ServiceResponse<GetCharacterDto>();
        var character = characters.FirstOrDefault(c => c.Id == id);
        ServiceResponse.Data = _mapper.Map<GetCharacterDto>(character);
        return ServiceResponse;


    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        try
        {

            var character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id) ?? throw new Exception($"Character with Id '{updatedCharacter.Id}' not found.");

            character.Name = updatedCharacter.Name;
            character.HitPoints = updatedCharacter.HitPoints;
            character.Strength = updatedCharacter.Strength;
            character.Defense = updatedCharacter.Defense;
            character.Intelligence = updatedCharacter.Intelligence;
            character.Class = updatedCharacter.Class;

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);

        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }
}

