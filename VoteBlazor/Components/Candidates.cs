using Microsoft.AspNetCore.Components;
using VoterApp.Domain.Models;

namespace VoteBlazor.Components.Pages.Components;

public class CandidateBase : ComponentBase
{
    //[Inject] protected NavigationManager? NavigationManager { get; set; }
    //[Inject] protected IConfiguration? Configuration { get; set; }
    [Inject] protected IVoterService? VoterService { get; set; }
    public List<Candidate>? CandidateList { get; set; } = [];
   
    public bool IsBusy { get; set; }
   
    public async Task BindCandidates()
    {
        CandidateList = await VoterService.GetCandidates();
    }

    protected override async Task OnInitializedAsync()
    {
        CandidateList = await VoterService.GetCandidates();
        //await base.OnInitializedAsync();
    }


        public void SetIsBusy(bool isBusy)
    {
        IsBusy = isBusy;
        if (isBusy)
        {
            // show loader
        }
    }
}