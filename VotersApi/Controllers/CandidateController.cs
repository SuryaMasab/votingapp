namespace VotersApi.Controllers;

[Route("api/[controller]")]
public class CandidateController(ICandidateRepository candidateRepository) : ControllerBase
{
    private readonly ICandidateRepository _candidateRepository = candidateRepository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Voter>>> GetCandidates()
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
        return candidate;
    }

    [HttpPost]
    public async Task<ActionResult<Candidate>> AddCandidate(Candidate candidate)
    {
        if(candidate == null)
        {
            return BadRequest();
        }
        var result = await _candidateRepository.AddCandidate(candidate);

        return CreatedAtAction(nameof(GetCandidate), new { id = candidate.Id }, candidate);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Candidate?>> UpdateCandidate(int id, Candidate candidate)
    {
        try
        {

            if (id != candidate.Id)
            {
                return BadRequest();
            }

            var result = await _candidateRepository.GetCandidate(id);
            if (result == null)
            {
                return NotFound("Candidate not found");
            }
            else
            {
                var updatedCandidate = await _candidateRepository.UpdateCandidate(candidate);
                return Ok(updatedCandidate);
            } 
        }
        catch (Exception ex)
        {
            throw ex;
        }
    } 

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteCandidate(int id)
    {
        var candidate = await _candidateRepository.GetCandidate(id);
        if (candidate == null)
        {
            return NotFound();
        }
        var isSuccess= await _candidateRepository.DeleteCandidate(id);
        if (isSuccess)
        {
            return Ok();
        }
        return NoContent();
    }
}
