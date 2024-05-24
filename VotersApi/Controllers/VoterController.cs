using VoterApp.Domain.Models;
using VotersApi.Repository;
using VotersApi.Repository.Interfaces;

namespace VotersApi.Controllers;

[Route("api/[controller]")]
public class VoterController(IVoterRepository VoterRepository, ILogger<VoterController> logger) : ControllerBase
{
    private readonly IVoterRepository _voterRepository = VoterRepository;

    private readonly ILogger<VoterController> _logger = logger;


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Voter>>> GetVoters()
    {
        var result = await _voterRepository.GetVoters();
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Voter>> GetVoter(int id)
    {       
        var voter = await _voterRepository.GetVoter(id);
        if (voter == null)
        {
            _logger.LogInformation("Get Voter By Id failed {id}", id);
            return NotFound();
        }
        return Ok(voter);
    }

    [HttpPost]
    public async Task<ActionResult<Voter>> AddVoter([FromBody] Voter? voter)
    {
        if (voter == null)
        {
            return BadRequest();
        }
        var result = await _voterRepository.AddVoter(voter);

        return CreatedAtAction(nameof(GetVoter), new { id = voter.Id }, voter);        
    }
     
}
