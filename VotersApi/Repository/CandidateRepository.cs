namespace VotersApi.Repository;

public class CandidateRepository : ICandidateRepository
{
    private VoteAppDbContext _dbContext { get; }
    public CandidateRepository(VoteAppDbContext context)
    {
        _dbContext = context;
    }
    public async Task<Candidate?> AddCandidate(Candidate candidate)
    {
        await _dbContext.Candidates.AddAsync(candidate);
        await _dbContext.SaveChangesAsync();
        return candidate;
    }

    public async Task<bool> DeleteCandidate(int candidateId)
    {
        var c = await _dbContext.Candidates.FindAsync(candidateId);
        if (c == null)
        {
            return false;
        }
        _dbContext.Candidates.Remove(c);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<Candidate?> GetCandidate(int candidateId)
    {
        return await _dbContext.Candidates.FindAsync(candidateId);
    }

    public async Task<IEnumerable<Candidate>> GetCandidates()
    {
        return await _dbContext.Candidates.ToListAsync();
    }

    public async Task<Candidate?> UpdateCandidate(Candidate candidate)
    {
         
            _dbContext.Candidates.Update(candidate);
            await _dbContext.SaveChangesAsync();
            return candidate;
        
        
    }
     
}
