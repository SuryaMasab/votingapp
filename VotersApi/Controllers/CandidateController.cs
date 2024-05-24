namespace VotersApi.Controllers;

[Route("api/[controller]")]
public class CandidateController(ICandidateRepository candidateRepository, ILogger<CandidateController> logger) : ControllerBase
{
    private readonly ICandidateRepository _candidateRepository = candidateRepository;
   
    private readonly ILogger<CandidateController> _logger = logger;
 

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Candidate>>> GetCandidates()
    {
        var result= await _candidateRepository.GetCandidates();
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Candidate>> GetCandidate(int id)
    {
        var candidate = await _candidateRepository.GetCandidate(id);
        if (candidate == null)
        {
            return NotFound();
        }
        return Ok(candidate);
    }

    [HttpPost]
    public async Task<ActionResult<Candidate>> AddCandidate([FromBody]Candidate candidate)
    {
        if(candidate == null)
        {
            return BadRequest();
        }
        var result = await _candidateRepository.AddCandidate(candidate);

        return CreatedAtAction(nameof(GetCandidate), new { id = candidate.Id }, candidate);
    }

}
