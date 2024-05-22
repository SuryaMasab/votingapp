﻿using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using VoterApp.Domain.Models;
using System;

namespace VoteBlazor.Services;

public class VoterService : IVoterService
{       
    private readonly HttpClient _httpClient;
    public VoterService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Candidate> AddCandidate(Candidate newCandidate)
    {
        try
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(newCandidate, jsonOptions), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/candidate", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var createdCandidate = await response.Content.ReadFromJsonAsync<Candidate>();
                return createdCandidate ?? newCandidate;
            }
            else
            {
                // Log the error message if the request was not successful
                Console.WriteLine($"Failed to add candidate. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during the API call
            Console.WriteLine($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
        }
        return null; 

    }

    public async Task<Voter> AddVoter(Voter newVoter)
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
                // Log the error message if the request was not successful
                Console.WriteLine($"Failed to add Voter. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during the API call
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
}