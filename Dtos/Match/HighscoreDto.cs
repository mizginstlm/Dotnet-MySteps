namespace DotnetSteps.Dtos.Match;

public class HighscoreDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Matches { get; set; }
    public int Victories { get; set; }
    public int Defeats { get; set; }
}