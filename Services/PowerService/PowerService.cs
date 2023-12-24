using System.Security.Claims;
using DotnetSteps.Data;
using DotnetSteps.Dtos.Character;
using DotnetSteps.Dtos.Power;
using Microsoft.EntityFrameworkCore;

namespace DotnetSteps.Services.PowerService
{
    public class PowerService : IPowerService
    {

        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public PowerService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
        }

        private string GetUserId() => _httpContextAccessor.HttpContext!.User
 .FindFirstValue(ClaimTypes.NameIdentifier)!;

        public async Task<ServiceResponse<GetCharacterDto>> AddPower(AddPowerDto newPower)
        {
            string userIdString = GetUserId();
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _context.Characters
                            .FirstOrDefaultAsync(c => c.Id == newPower.CharacterId &&
                                c.User!.Id == new Guid(userIdString));

                if (character is null)
                {
                    response.Success = false;
                    response.Message = "Character not found.";
                    return response;
                }

                var power = new Power
                {
                    Name = newPower.Name,
                    Damage = newPower.Damage,
                    Character = character
                };

                _context.Powers.Add(power);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
