global using AutoMapper;
using System.Reflection;
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



    //IMapper ve DataContext dependencies olarak gelir
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

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters([FromQuery] string? filterOn, [FromQuery] string? filterQuery, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        if (_context.Characters != null)
        {
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

            //burada sadece name için filter var
            // if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            // {
            //     if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
            //     {
            //         serviceResponse.Data = dbCharacters.Where(x => x.Name.Contains(filterQuery)).Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            //     }
            // }

            //burada her property için filter yaparız
            var propertyName = filterOn;
            if (propertyName != null)
            {
                if (filterQuery == null) { throw new Exception("Invalid or null query name"); }
                else
                {
                    var property = typeof(Character).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    // Use the property to filter the data
                    serviceResponse.Data = dbCharacters
                        .Where(x => property.GetValue(x)?.ToString()?.Contains(filterQuery, StringComparison.OrdinalIgnoreCase) ?? false)
                        .Select(c => _mapper.Map<GetCharacterDto>(c))
                        .ToList();
                }

            }
            // Sorting for a spesific thing
            // if (string.IsNullOrWhiteSpace(sortBy) == false)
            // {
            //     if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            //     {
            //         serviceResponse.Data = isAscending ? dbCharacters.OrderBy(x => x.Name).Select(c => _mapper.Map<GetCharacterDto>(c)).ToList(); : dbCharacters.OrderByDescending(x => x.Name).Select(c => _mapper.Map<GetCharacterDto>(c)).ToList(); ;
            //     }
            //     else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
            //     {
            //         serviceResponse.Data = isAscending ? dbCharacters.OrderBy(x => x.Defense).Select(c => _mapper.Map<GetCharacterDto>(c)).ToList(); : dbCharacters.OrderByDescending(x => x.Defense).Select(c => _mapper.Map<GetCharacterDto>(c)).ToList(); ;
            //     }
            // }

            //sorting for everything
            string sortingPropertyName = sortBy;
            if (sortingPropertyName != null)
            {
                var sortingProperty = typeof(Character).GetProperty(sortingPropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                sortingProperty = typeof(GetCharacterDto).GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (sortingProperty != null)
                {
                    serviceResponse.Data = isAscending
                        ? serviceResponse.Data.OrderBy(x => sortingProperty.GetValue(x)).ToList()
                        : serviceResponse.Data.OrderByDescending(x => sortingProperty.GetValue(x)).ToList();
                }
            }

            // Pagination this pagination is just good for smalll data(offset pagination)
            var skipResults = (pageNumber - 1) * pageSize;
            var characters = serviceResponse.Data;
            var paginatedCharacters = characters.Skip(skipResults).Take(pageSize).ToList();
            var paginatedServiceResponse = new ServiceResponse<List<GetCharacterDto>>
            {
                Data = paginatedCharacters,
                Success = serviceResponse.Success,
                Message = serviceResponse.Message
            };

            return paginatedServiceResponse;
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

            var character = await _context.Characters.FindAsync(id) ?? throw new Exception($"Character with that Id not found.");

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

