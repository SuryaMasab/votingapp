using Microsoft.AspNetCore.Components;
using VoterApp.Domain.Models;

namespace VoteBlazor.Components.Pages.Components;

public class CandidateBase : ComponentBase
{ 
    [Inject] protected IVoterService? VoterService { get; set; }    
    [Parameter] public List<Candidate>? CandidateList { get; set; } = [];
    [Parameter] public bool ShowAddNewItem { get; set; }   
    
    public List<RenderFragment> addComponents = new List<RenderFragment>();

    public async Task BindCandidates()
    { 

        CandidateList = await VoterService.GetCandidates();
    }     

    public void AddNewComponent()
    {
        if (addComponents.Count == 0)
        {
                addComponents.Add(builder =>
                {
                    builder.OpenComponent(0, typeof(AddComponent));
                    builder.AddComponentParameter(1, "EntityType", "Candidate");
                    builder.AddComponentParameter(2, "CandidatesComponent", this); // Pass the reference
                    builder.AddComponentParameter(3, "ShowAddNewItem", ShowAddNewItem);
                    builder.CloseComponent();
                }); 
                ShowAddNewItem = true; 
        }
    }
 

    public async Task RefreshCandidatesData()
    {        
        await BindCandidates();
        addComponents.Clear();
        StateHasChanged();
        ShowAddNewItem = false;
    }
}