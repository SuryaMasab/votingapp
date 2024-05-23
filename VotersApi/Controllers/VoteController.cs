namespace VotersApi.Controllers;

[Route("api/[controller]")]
public class VoteController(IVoterRepository voterRepository, 
                ICandidateRepository candidateRepository,
                ICastVoteRepository castVoteRepository,
                ILogger<VoteController> logger ) : ControllerBase
{
    private readonly IVoterRepository _voterRepository = voterRepository;
    private readonly ICandidateRepository _candidateRepository = candidateRepository;
    private readonly ICastVoteRepository _castVoteRepository = castVoteRepository;
    private readonly ILogger<VoteController> _logger = logger;


    [HttpPost]
    public async Task<ActionResult<bool>> CastVote([FromBody] Vote newVote)
    {
        try
        {
            if (newVote == null)
            {
                return BadRequest();
            }
            var voter = await _voterRepository.GetVoter(newVote.VoterId);
            if (voter == null || voter.HasVoted)
            {
                return BadRequest("Invalid voter or voter has already voted.");
            }

            var candidate = await _candidateRepository.GetCandidate(newVote.CandidateId);
            if (candidate == null)
            {
                return BadRequest("Invalid candidate.");
            }

            voter.HasVoted = true;
            candidate.VotesCount += 1;

            var result = _castVoteRepository.CastVote(newVote);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogCritical($"Exception Occured CastVote API {ex.Message} {ex.StackTrace}");
        }
        return Ok(false);
    }
}
