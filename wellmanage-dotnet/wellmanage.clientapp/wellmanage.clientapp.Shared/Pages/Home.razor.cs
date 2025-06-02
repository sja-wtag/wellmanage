using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using wellmanage.clientapp.Shared.Services;
using wellmanage.shared.Models;

namespace wellmanage.clientapp.Shared.Pages
{
    public partial class Home
    {
        [Inject]
        AttendenceService attendenceService { get; set; }
        AttendanceStatus currentAttendenceStatus;
        protected override void OnInitialized()
        {
           LoadAttendenceStatus();
        }

        public async Task LoadAttendenceStatus()
        {
            currentAttendenceStatus = await attendenceService.GetAttendenceStatus();
        }
    }
}
