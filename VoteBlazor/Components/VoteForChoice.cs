using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using VoterApp.Domain.Models;

namespace VoteBlazor.Components.Pages.Components;

public class VoteForChoiceBase: ComponentBase
{
    #region [Inject]
    [Inject] protected IVoterService VoterService { get; set; }

    #endregion


    #region [Parameters]

    public List<VoterApp.Domain.Models.Voter> Voters { get; set; }
    public List<VoterApp.Domain.Models.Candidate> Candidates { get; set; }
    [Parameter] public EventCallback<bool> RefrehVoteCount {  get; set; }

    //[Parameter] public EventCallback<bool> ReBindLists { get; set; }

    #endregion


    #region Properties

    public VoterApp.Domain.Models.Vote ChoiceOfVoter = new Vote();

    #endregion


    #region Events

    protected async override Task OnInitializedAsync() => await LoadVotersAndCandidates();


    #endregion

    #region Methods
    private async Task LoadVotersAndCandidates()
    {          
        Candidates = await VoterService.GetCandidates() ?? new();

        Voters = await VoterService.GetVoters() ?? new();
        
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
    public async Task HandleNewItemsAdded()
    {
        await LoadVotersAndCandidates();
    }

    #endregion
}
