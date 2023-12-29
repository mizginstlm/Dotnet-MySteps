namespace DotnetSteps.Dtos.Match;

public class AbilityAttackDto
{
    public Guid AttackerId { get; set; }
    public Guid OpponentId { get; set; }
    public int AbilityId { get; set; }
}