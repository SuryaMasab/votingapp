using Microsoft.AspNetCore.Components;
using VoterApp.Domain.Models;

namespace VoteBlazor.Components.Pages.Components;

public class CandidateBase : ComponentBase
{
    #region Inject
    [Inject] protected IVoterService? VoterService { get; set; }

    #endregion

    #region Parameters

    [Parameter] public List<Candidate>? CandidateList { get; set; } = [];
    [Parameter] public bool ShowAddNewItem { get; set; }    
    [Parameter] public EventCallback OnItemAdded { get; set; }

    #endregion

    #region ElementRefs

    public List<RenderFragment> addComponents = new List<RenderFragment>();

    #endregion

    #region Methods
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
                    builder.AddComponentParameter(4, "OnCandidateAdded", EventCallback.Factory.Create(this, HandleCandidateAdded));                    
                    builder.CloseComponent();
                }); 
                ShowAddNewItem = true; 
        }
    }

    private async Task HandleCandidateAdded()
    {      
        if(OnItemAdded.HasDelegate)
        {
            await OnItemAdded.InvokeAsync(this);
        }
        await BindCandidates();
        StateHasChanged();
    }

    public async Task RefreshCandidatesData()
    {        
        await BindCandidates();

        addComponents.Clear();
        StateHasChanged();
        ShowAddNewItem = false;
    }

    #endregion
}