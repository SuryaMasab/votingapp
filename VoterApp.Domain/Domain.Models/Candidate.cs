namespace VoterApp.Domain.Models;

public class Candidate : AuditEntityBase
{
    public string Name { get; set; }

    public int VotesCount { get; set; }
    public ICollection<Vote>? Votes { get; set; }
}
