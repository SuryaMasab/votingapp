using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Routing.Matching;
using VoteBlazor.Components.Pages.Components;
using VoterApp.Domain.Models;
using static System.Net.WebRequestMethods;

namespace VoteBlazor.Components;

public class AddComponentBase:ComponentBase
{
    [Parameter] public string? EntityType { get; set; }
    [Inject] protected IVoterService? VoterService { get; set; }
    public EntityItem NewItem { get; set; } = new(); 
    [Parameter] public VoteBlazor.Components.Candidates? CandidatesComponent { get; set; }

    [Parameter] public VoteBlazor.Components.Voter? VotersComponent { get; set; }
    public async Task HandleValidSubmit()
    {
        if(EntityType == "Candidate")
        {
            var newCandidate = new Candidate
            {
                Name = NewItem.Name
            };
            newCandidate = await VoterService.AddCandidate(newCandidate);
            if (newCandidate.Id > 0 && CandidatesComponent!=null)
            {
                await CandidatesComponent.RefreshCandidatesData(); 
                base.StateHasChanged();
            }

        }
        else if (EntityType == "Voter")
        {
            var newVoter = new VoterApp.Domain.Models.Voter
            {
                Name = NewItem.Name
            };
            newVoter= await VoterService.AddVoter(newVoter);
            if (newVoter != null && newVoter.Id > 0 && VotersComponent!=null)
            {
                await VotersComponent.RefreshVotersData();
                base.StateHasChanged();

            }
        }  
    } 

}
public class EntityItem
{
    public string Name { get; set; }
}