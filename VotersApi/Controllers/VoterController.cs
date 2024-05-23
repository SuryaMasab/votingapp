using VoterApp.Domain.Models;

namespace VotersApi.Controllers;

[Route("api/[controller]")]
public class VoterController(VoteAppDbContext context, ILogger<VoteController> logger) : ControllerBase
{
    private readonly VoteAppDbContext _context = context;
    private readonly ILogger<VoteController> _logger = logger;
     
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Voter>>> GetVoters()
    {
        return await _context.Voters.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Voter>> GetVoter(int id)
    {
        var voter = await _context.Voters.FindAsync(id);
        if (voter == null)
        {
            _logger.LogInformation("Get Voter By Id failed {id}", id);
            return NotFound();
        }
        return voter;
    }

    [HttpPost]
    public async Task<ActionResult<Voter>> AddVoter([FromBody] Voter? voter)
    {
        if (voter == null)
        {
            _logger.LogInformation("Invalid Add Voter Object");
            return NotFound();
        }
        _context.Voters.Add(voter);
        await _context.SaveChangesAsync();       
        return CreatedAtAction(nameof(GetVoter), new { id = voter.Id }, voter);
    }


    [Obsolete]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVoter(int id, Voter voter)
    {
        if (id != voter.Id)
        {
            _logger.LogInformation("Update/Put Voter could not be found");
            return BadRequest();
        }
        _context.Entry(voter).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {             
            _logger.LogInformation($"Exception Occured Vide update {ex.Message} {ex.StackTrace}");
        }
        return NoContent();
    }

    [Obsolete]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVoter(int id)
    {
        var voter = await _context.Voters.FindAsync(id);
        if (voter == null)
        {
            return NotFound();
        }
        _context.Voters.Remove(voter);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
