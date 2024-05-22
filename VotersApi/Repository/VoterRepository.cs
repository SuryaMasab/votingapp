namespace VotersApi.Repository;

public class VoterRepository: IVoterRepository
{
    private VoteAppDbContext DbContext { get; }
    public VoterRepository(VoteAppDbContext context)
    {
        DbContext = context;
    }
    public async Task<Voter?> AddVoter(Voter voter)
    {
        await DbContext.Voters.AddAsync(voter);
        await DbContext.SaveChangesAsync();
        return voter;
    }

    public async Task<bool> DeleteVoter(int voterId)
    {
        var c = await DbContext.Voters.FindAsync(voterId);
        if (c == null)
        {
            return false;
        }
        DbContext.Voters.Remove(c);
        await DbContext.SaveChangesAsync();

        return true;
    }

    public async Task<Voter?> GetVoter(int voterId)
    {
        return await DbContext.Voters.FindAsync(voterId);
    }

    public async Task<IEnumerable<Voter>> GetVoters()
    {
        return await DbContext.Voters.ToListAsync();
    }

    public async Task<Voter?> UpdateVoter(Voter voter)
    {
        DbContext.Voters.Update(voter);
        await DbContext.SaveChangesAsync();
        return voter;  
    }
}
