
using VoterApp.Domain.Models;

namespace VoteBlazor.Services.Interfaces;

public interface IVoterService
{
    Task<List<Candidate>> GetCandidates();
    Task<List<Voter>> GetVoters();
    Task<Voter> AddVoter(Voter newVoter);
    Task<Candidate> AddCandidate(Candidate newCandidate);
}
