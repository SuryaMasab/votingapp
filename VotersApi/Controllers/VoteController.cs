

namespace VotersApi.Controllers;

[Route("api/[controller]")]
public class VoteController(IVoterRepository voterRepository, 
                ICandidateRepository candidateRepository,
                ICastVoteRepository castVoteRepository) : ControllerBase
{
    private readonly IVoterRepository _voterRepository = voterRepository;
    private readonly ICandidateRepository _candidateRepository = candidateRepository;
    private readonly ICastVoteRepository _castVoteRepository = castVoteRepository;


    [HttpPost]
    public async Task<IActionResult> CastVote(int voterId, int candidateId)
    {
        var voter = await _voterRepository.GetVoter(voterId);
        if (voter == null || voter.HasVoted)
        {
            return BadRequest("Invalid voter or voter has already voted.");
        }

        var candidate = await _candidateRepository.GetCandidate(candidateId);
        if (candidate == null)
        {
            return BadRequest("Invalid candidate.");
        }

        var vote = new Vote
        {
            VoterId = voterId,
            CandidateId = candidateId, 

        };

        voter.HasVoted = true;
        candidate.VotesCount += 1;

        _castVoteRepository.CastVote(vote); 

        return Ok();
    }
}
