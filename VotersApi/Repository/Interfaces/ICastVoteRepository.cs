namespace VotersApi.Repository.Interfaces;

public interface ICastVoteRepository
{
    public bool CastVote(Vote vote);
}
