using VoterApp.Domain.Models;

namespace VoteBlazor.Services;

public class VoterService : IVoterService
{       
    private readonly HttpClient _httpClient;
    public VoterService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Candidate>> GetCandidates()
    {
        return await _httpClient.GetFromJsonAsync<List<Candidate>>("api/candidate") ?? new();
    }

    public async Task<List<Voter>> GetVoters()
    {       
        return await _httpClient.GetFromJsonAsync<List<Voter>>("api/voter") ?? new();
    }
}