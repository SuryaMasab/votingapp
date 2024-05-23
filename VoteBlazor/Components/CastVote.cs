using Microsoft.AspNetCore.Components;
using VoterApp.Domain.Models;

namespace VoteBlazor.Components.Pages.Components;

public partial class CastVoteBase : ComponentBase
{    
    [Inject] protected NavigationManager? NavigationManager { get; set; }
    [Inject] protected IVoterService? VoterService { get; set; }
    
    public Candidates? CandidatesRef { get; set;}
    public Voter? VotersRef { get; set;}    
    public VoteForChoice? VoteForChoiceRef { get; set; }

    [Parameter] public List<VoterApp.Domain.Models.Candidate>? CandidateList { get; set; }
    [Parameter] public List<VoterApp.Domain.Models.Voter>? VoterList { get; set; }
  
    protected override async Task OnInitializedAsync()
    {       

        await BindCandidates();

        await BindVoters();
    }

    public async Task BindCandidates()
    {
        var result= await VoterService!.GetCandidates();
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
        var result = await VoterService!.GetVoters();
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

    public async Task RefreshCandidateVotes()
    {
        await BindCandidates();
        await BindVoters();  
    }

    public async Task RefreshVoteForChoiceDropDowns()
    {
        if (VoteForChoiceRef != null)
        {
            await VoteForChoiceRef.HandleNewItemsAdded();
            StateHasChanged();
        }
    }
 
}
