using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetSteps.Services.CharacterService;

public class CharacterService : ICharacterService
{

    private static List<Character> characters = new List<Character>
    {
        new Character(),
        new Character{Id=1, Name="Samuel"}
};

    public List<Character> GetAllCharacters()
    {
        return characters;
    }

    public Character GetCharacterById(int id)
    {
        var character = characters.FirstOrDefault(c => c.Id == id);
        if (character is not null)
            return character;

        throw new Exception("not found character");


    }


    public List<Character> AddCharacter(Character newCharacter)
    {
        characters.Add(newCharacter);
        return characters;
    }

}