namespace VotersApi.Controllers;

[Route("api/[controller]")]
public class VoterController(VoteAppDbContext context) : ControllerBase
{
    private readonly VoteAppDbContext _context = context;

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
            return NotFound();
        }
        return voter;
    }

    [HttpPost]
    public async Task<ActionResult<Voter>> AddVoter([FromBody] Voter voter)
    {
        _context.Voters.Add(voter);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetVoter), new { id = voter.Id }, voter);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVoter(int id, Voter voter)
    {
        if (id != voter.Id)
        {
            return BadRequest();
        }
        _context.Entry(voter).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Voters.Any(e => e.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }

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
