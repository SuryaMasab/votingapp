using System.Text.Json.Serialization;

namespace VoterApp.Domain.Models;
public class Vote : AuditEntityBase
{    
    public int VoterId { get; set; }
    public int CandidateId { get; set; }

    //Nav properties
    [JsonIgnore]
    public  Voter Voter { get; set; }
    [JsonIgnore]
    public  Candidate Candidate { get; set; }
}
