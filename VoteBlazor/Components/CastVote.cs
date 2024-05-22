using Microsoft.AspNetCore.Components;
using VoterApp.Domain.Models;

namespace VoteBlazor.Components.Pages.Components;

public partial class CastVoteBase : ComponentBase
{
    [Inject] protected HttpClient? HttpClient { get; set; }
    [Inject] protected NavigationManager? NavigationManager { get; set; }
    [Inject] protected IConfiguration? Configuration { get; set; }
    public List<Candidate> Candidates { get; set; }
    public List<Voter> Voters { get; set; }
    private string? baseUrl = string.Empty;
    private HttpClient? _httpClient;

    public CastVoteBase()
    {
        Candidates = [];
        Voters = [];
        baseUrl = Configuration?.GetSection("ApiUrl").GetValue<string>("Url") ?? string.Empty;
        _httpClient =  new HttpClient();
    }
    protected override async Task OnInitializedAsync()
    {
        // Simulate asynchronous loading to demonstrate streaming rendering
        await Task.Delay(500);

        var startDate = DateOnly.FromDateTime(DateTime.Now);
        //await GetCandidates();

        //await GetVoters();
    }

    //private async Task GetCandidates()
    //{
    //    var dataRequest = await _httpClient.GetAsync($"{baseUrl}api/Candidate");

    //    if (dataRequest.IsSuccessStatusCode)
    //    {
    //        var jsonString = JsonDocument.Parse(await dataRequest.Content.ReadAsStreamAsync());
    //        var candidateResult = jsonString.Deserialize<List<Candidate>>();
    //        if (candidateResult != null)
    //        {
    //            CandidateList = candidateResult;
    //        }
    //        else
    //        {
    //            throw new Exception("Error loading data.");
    //        }
    //    }
    //    else
    //    {
    //        throw new Exception("Error sending request.");
    //    }
    //} 

}
