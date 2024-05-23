namespace VotersApi.Repository.Interfaces;

public class CastVoteRepository : ICastVoteRepository
{
    private VoteAppDbContext _dbContext { get; }
    public CastVoteRepository(VoteAppDbContext context)
    {
        _dbContext = context;
    } 
    public bool CastVote(Vote vote)
    {
        _dbContext.Votes.Add(vote);
        _dbContext.SaveChanges();
        return true;
    }
}
