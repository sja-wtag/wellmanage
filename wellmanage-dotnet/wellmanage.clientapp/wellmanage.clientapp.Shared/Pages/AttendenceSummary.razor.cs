using Microsoft.AspNetCore.Components;
using wellmanage.clientapp.Shared.Services;
using wellmanage.shared.Models;

namespace wellmanage.clientapp.Shared.Pages;

public partial class AttendenceSummary : ComponentBase
{
    [Inject]
    public AttendenceService attendenceService { get; set; }

    private List<AttendanceResponse> _attendanceResponses = [];
    protected override void OnInitialized()
    {
        LoadAttendencSummary();
    }
    
    public async Task LoadAttendencSummary()
    {
        _attendanceResponses = await attendenceService.GetAttendances();
    }
}