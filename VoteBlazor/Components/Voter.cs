using Microsoft.AspNetCore.Components;

namespace VoteBlazor.Components.Pages.Components;

public class VoterBase : ComponentBase
{  
    [Inject] protected IVoterService? VoterService { get; set; }
    [Parameter] public List<VoterApp.Domain.Models.Voter>? VoterList { get; set; }
    [Parameter] public bool ShowAddNewItem { get; set; }
    public VoterBase()
    {
        VoterList = [];       
    }
    public async Task BindVoters()
    {
        VoterList = await VoterService.GetVoters() ?? new();
    }
     

    public List<RenderFragment> addComponents = new List<RenderFragment>();

    public void AddNewComponent()
    {
        if (addComponents.Count == 0)
        {
            addComponents.Add(builder =>
            {
                builder.OpenComponent(0, typeof(AddComponent));
                builder.AddComponentParameter(1, "EntityType", "Voter");
                builder.AddComponentParameter(2, "VotersComponent", this); // Pass the reference
                builder.AddComponentParameter(3, "ShowAddNewItem", ShowAddNewItem);
                builder.CloseComponent();
            });
            ShowAddNewItem = true;
        }
    }
    public async Task RefreshVotersData()
    {
        await BindVoters();
        StateHasChanged();
        addComponents.Clear();
        ShowAddNewItem = false;
    }

}
