namespace VotersApi.Repository.Interfaces;

public class CastVoteRepository : ICastVoteRepository
{
    private VoteAppDbContext _dbContext { get; }
    public CastVoteRepository(VoteAppDbContext context)
    {
        _dbContext = context;
    } 
    public void CastVote(Vote vote)
    {
        _dbContext.Votes.Add(vote);
        _dbContext.SaveChanges();
    }
}
