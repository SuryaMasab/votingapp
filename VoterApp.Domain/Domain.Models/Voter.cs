namespace VoterApp.Domain.Models;

public class Voter : AuditEntityBase
{ 
    public string Name { get; set; }= string.Empty;
    public bool HasVoted { get; set; }

    public ICollection<Vote>? Votes { get; set; }
}
