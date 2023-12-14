global using AutoMapper;
using DotnetSteps.Data;
using DotnetSteps.Dtos.Character;
using DotnetSteps.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotnetSteps.Services.CharacterService;

public class CharacterService : ICharacterService
{
    private static List<Character> characters = new List<Character>
    {
        //buraya yeni karakterler eklemiştim boş data olmasın diye, ef kullanmadan önce
    };

    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CharacterService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
        var ServiceResponse = new ServiceResponse<List<GetCharacterDto>>();
        var character = _mapper.Map<Character>(newCharacter);

        _context.Characters.Add(_mapper.Map<Character>(character));
        await _context.SaveChangesAsync();
        ServiceResponse.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        return ServiceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(Guid id)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        try
        {

            var dbCharacter = await _context.Characters.FindAsync(id) ?? throw new Exception($"Character with Id '{id}' not found.");

            _context.Characters.Remove(dbCharacter);
            await _context.SaveChangesAsync();
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        if (_context.Characters != null)
        {
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            await _context.SaveChangesAsync();
        }
        else
        {
            serviceResponse.Message = "Characters data is not available.";
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(Guid id)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        try
        {
            var dbCharacter = await _context.Characters.FindAsync(id) ?? throw new Exception($"Character with Id '{id}' not found.");
            // var character = characters.FirstOrDefault(c => c.Id == id) ?? throw new Exception($"Character with Id '{id}' not found.");

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;



    }


    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(Guid id, UpdateCharacterDto updatedCharacter)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        try
        {

            var character = await _context.Characters.FindAsync(id) ?? throw new Exception($"Character with Id '{updatedCharacter.Id}' not found.");

            character.Name = updatedCharacter.Name;
            character.HitPoints = updatedCharacter.HitPoints;
            character.Strength = updatedCharacter.Strength;
            character.Defense = updatedCharacter.Defense;
            character.Intelligence = updatedCharacter.Intelligence;
            character.Class = updatedCharacter.Class;
            character.ImageUri = updatedCharacter.ImageUri;
            await _context.SaveChangesAsync();
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

