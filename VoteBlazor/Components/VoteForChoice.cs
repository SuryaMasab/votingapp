using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using VoterApp.Domain.Models;

namespace VoteBlazor.Components.Pages.Components;

public class VoteForChoiceBase: ComponentBase
{
    [Inject] protected IVoterService VoterService { get; set; }

    public VoterApp.Domain.Models.Vote ChoiceOfVoter = new Vote();
    public List<VoterApp.Domain.Models.Voter> Voters = new();
    public List<VoterApp.Domain.Models.Candidate> Candidates = new();
    [Parameter] public EventCallback<bool> RefrehVoteCount {  get; set; }
    protected async override Task OnInitializedAsync() => await LoadVotersAndCandidates();
 
    private async Task LoadVotersAndCandidates()
    { 
        Voters = await VoterService.GetVoters() ?? new();
        Candidates = await VoterService.GetCandidates() ?? new();
    }

    public async Task HandleValidSubmit()
    {        
        await SubmitVoteAsync(ChoiceOfVoter);
    
        await LoadVotersAndCandidates();

        ClearForm();
    }

    public void ClearForm()
    {
        ChoiceOfVoter = new Vote();
    }     

    private async Task SubmitVoteAsync(Vote postedVote)
    {
        var result = await VoterService.RecordVotersChoice(postedVote); 
        if(result)
        {
            await LoadVotersAndCandidates();
            if(RefrehVoteCount.HasDelegate)
            {
                await RefrehVoteCount.InvokeAsync();
            }
        }
        
    }
}
