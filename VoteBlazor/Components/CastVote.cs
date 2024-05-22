﻿using Microsoft.AspNetCore.Components;
using VoterApp.Domain.Models;

namespace VoteBlazor.Components.Pages.Components;

public partial class CastVoteBase : ComponentBase
{    
    [Inject] protected NavigationManager? NavigationManager { get; set; }
    [Inject] protected IVoterService? VoterService { get; set; }
    
    public Candidates CandidatesRef { get; set;}
    public Voter VotersRef { get; set;}

    public List<VoterApp.Domain.Models.Candidate> CandidateList { get; set; }
    public List<VoterApp.Domain.Models.Voter> VoterList { get; set; } 


    public CastVoteBase()
    {
        CandidateList = new List<Candidate>();
        VoterList = new List<VoterApp.Domain.Models.Voter>();
    }
    protected override async Task OnInitializedAsync()
    {
        // Simulate asynchronous loading to demonstrate streaming rendering
        await Task.Delay(500);

        var startDate = DateOnly.FromDateTime(DateTime.Now);
        await BindCandidates();

        await BindVoters();
    }

    public async Task BindCandidates()
    {
        var result= await VoterService.GetCandidates();
        if(result != null)
        {
            CandidateList = result;
            StateHasChanged();
        }
        else
        {
            CandidateList = new();
        }

    }

    public async Task BindVoters()
    {
        var result = await VoterService.GetVoters();
        if (result != null)
        {
            VoterList = result;
            StateHasChanged();
        }
        else
        {
            VoterList = new();
        }
        
    }

}
