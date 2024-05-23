namespace VotersApi.Repository.Interfaces;

public interface ICandidateRepository
{
    public Task<Candidate?> AddCandidate(Candidate candidate);
    public Task<Candidate?> GetCandidate(int candidateId);
    public Task<IEnumerable<Candidate>> GetCandidates();
    public Task<Candidate?> UpdateCandidate(Candidate candidate);
    public Task<bool> DeleteCandidate(int candidateId);
}
