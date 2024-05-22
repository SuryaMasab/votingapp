namespace VotersApi.Repository.Interfaces;

public interface IVoterRepository
{
    public Task<Voter?> AddVoter(Voter voter);
    public Task<Voter?> GetVoter(int voterId);
    public Task<IEnumerable<Voter>> GetVoters();
    public Task<Voter?> UpdateVoter(Voter voter);
    public Task<bool> DeleteVoter(int voterId);
}
