namespace VotersApi.Repository;

public class CandidateRepository(VoteAppDbContext context) : ICandidateRepository
{
    private VoteAppDbContext DbContext { get; } = context;

    public async Task<Candidate?> AddCandidate(Candidate candidate)
    {
        await DbContext.Candidates.AddAsync(candidate);
        await DbContext.SaveChangesAsync();
        return candidate;
    }

    public async Task<bool> DeleteCandidate(int candidateId)
    {
        var c = await DbContext.Candidates.FindAsync(candidateId);
        if (c == null)
        {
            return false;
        }
        DbContext.Candidates.Remove(c);
        await DbContext.SaveChangesAsync();

        return true;
    }

    public async Task<Candidate?> GetCandidate(int candidateId)
    {
        return await DbContext.Candidates.FindAsync(candidateId);
    }

    public async Task<IEnumerable<Candidate>> GetCandidates()
    {
        return await DbContext.Candidates.ToListAsync();
    }

    public async Task<Candidate?> UpdateCandidate(Candidate candidate)
    {
         
            DbContext.Candidates.Update(candidate);
            await DbContext.SaveChangesAsync();
            return candidate;
        
        
    }
     
}
