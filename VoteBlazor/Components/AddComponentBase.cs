using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Routing.Matching;
using VoteBlazor.Components.Pages.Components;
using VoterApp.Domain.Models;
using static System.Net.WebRequestMethods;

namespace VoteBlazor.Components;

public class AddComponentBase:ComponentBase
{
    #region [Parameters]

    [Parameter] public string? EntityType { get; set; }
    [Parameter] public bool ShowAddNewItem { get; set; }
    [Parameter] public VoteBlazor.Components.Candidates? CandidatesComponent { get; set; }
    [Parameter] public VoteBlazor.Components.Voter? VotersComponent { get; set; }
    [Parameter] public EventCallback OnCandidateAdded { get; set; }
    [Parameter] public EventCallback OnVoterAdded { get; set; }
    public string? ErrorMessage { get; set; }
    #endregion

    #region {Inject]
    [Inject] protected IVoterService? VoterService { get; set; }

    #endregion

    #region Properties
    public EntityItem NewItem { get; set; } = new();

    #endregion

    #region Methods

    public async Task HandleValidSubmit()
    { 
        if(EntityType == "Candidate" && !string.IsNullOrEmpty(NewItem.Name))
        {
            ErrorMessage = string.Empty;
            var newCandidate = new Candidate
            {
                Name = NewItem.Name
            };
            newCandidate = await VoterService.AddCandidate(newCandidate);
            if (newCandidate.Id > 0 && CandidatesComponent!=null)
            {             
                await CandidatesComponent.RefreshCandidatesData();
                if(OnCandidateAdded.HasDelegate)
                {
                    await OnCandidateAdded.InvokeAsync();
                }
                StateHasChanged();
                ShowAddNewItem = false;
            }

        }
        else if (EntityType == "Voter" && !string.IsNullOrEmpty(NewItem.Name))
        {
            ErrorMessage = string.Empty;
            var newVoter = new VoterApp.Domain.Models.Voter
            {
                Name = NewItem.Name
            };
            newVoter= await VoterService.AddVoter(newVoter);
            if (newVoter != null && newVoter.Id > 0 && VotersComponent!=null)
            {
                await VotersComponent.RefreshVotersData();
                if (OnVoterAdded.HasDelegate)
                {
                    await OnVoterAdded.InvokeAsync();
                }
                StateHasChanged();
                ShowAddNewItem = false;
            }
        }
        else
        {
            if(string.IsNullOrEmpty(NewItem.Name))
            {
                ErrorMessage = "Invalid Name!";
                StateHasChanged();
            }
        }
    }
    public void ClearTextbox()
    {
        NewItem.Name = string.Empty;
    }

    #endregion
}
public class EntityItem
{
    public string Name { get; set; }
}