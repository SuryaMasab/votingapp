using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using VoterApp.Domain.Models;


namespace VoteBlazor.Services;

public class VoterService : IVoterService
{       
    private readonly HttpClient _httpClient;
    public VoterService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public JsonSerializerOptions GetJsonOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }

    public async Task<Candidate?> AddCandidate(Candidate newCandidate, JsonSerializerOptions jsonOptions)
    {
        try
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(newCandidate, jsonOptions), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/candidate", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var createdCandidate = await response.Content.ReadFromJsonAsync<Candidate>();
                return createdCandidate ?? newCandidate;
            }
            else
            {                
                Console.WriteLine($"Failed to add candidate. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {             
            Console.WriteLine($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
        }
        return newCandidate; 

    }

    public async Task<Voter?> AddVoter(Voter newVoter)
    {


        try
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(newVoter, jsonOptions), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/voter", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var createdVoter = await response.Content.ReadFromJsonAsync<Voter>();

                return createdVoter ?? newVoter;
            }
            else
            {              
                Console.WriteLine($"Failed to add Voter. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {           
            Console.WriteLine($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
        }
        return null; 
    }  

    public async Task<List<Candidate>> GetCandidates()
    {
        return await _httpClient.GetFromJsonAsync<List<Candidate>>("api/candidate") ?? new();
    }

    public async Task<List<Voter>> GetVoters()
    {       
        return await _httpClient.GetFromJsonAsync<List<Voter>>("api/voter") ?? new();
    }

    public async Task<bool> RecordVotersChoice(Vote newPostedVote)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(newPostedVote);
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };


            var jsonContent = new StringContent(JsonSerializer.Serialize(newPostedVote, jsonOptions), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/vote", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var postedVoteResult = await response.Content.ReadFromJsonAsync<bool>();
                return postedVoteResult;
            }
            else
            {              
                Console.WriteLine($"Failed to add candidate. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {           
            Console.WriteLine($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
        }
        return false;
    }
     
}