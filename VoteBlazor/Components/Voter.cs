using Microsoft.AspNetCore.Components;

namespace VoteBlazor.Components.Pages.Components;

public class VoterBase : ComponentBase
{ 
    [Inject] protected NavigationManager? NavigationManager { get; set; }
    //[Inject] protected IConfiguration? Configuration { get; set; }
    [Inject] protected IVoterService? VoterService { get; set; }
    public List<VoterApp.Domain.Models.Voter>? Voters { get; set; }
   

    public VoterBase()
    {
        Voters = [];       
    }
    public async Task GetVoters()
    {        
        Voters = await VoterService.GetVoters(); 
    }
}
