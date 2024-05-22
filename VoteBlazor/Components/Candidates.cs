using Microsoft.AspNetCore.Components;
using VoterApp.Domain.Models;

namespace VoteBlazor.Components.Pages.Components;

public class CandidateBase : ComponentBase
{ 
    [Inject] protected IVoterService VoterService { get; set; }    
    [Parameter] public List<Candidate>? CandidateList { get; set; } = [];
    public bool IsBusy { get; set; }
   
    public List<RenderFragment> addComponents = new List<RenderFragment>();

    public async Task BindCandidates()
    { 

        CandidateList = await VoterService.GetCandidates();
    }     

    public void AddNewComponent()
    {
        addComponents.Add(builder =>
        {
            builder.OpenComponent(0, typeof(AddComponent));
            builder.AddComponentParameter(1, "EntityType", "Candidate");
            builder.AddComponentParameter(2, "CandidatesComponent", this); // Pass the reference
            builder.CloseComponent();
        });
    }     

    public async Task RefreshCandidatesData()
    {        
        await BindCandidates();
        StateHasChanged();
    }
}