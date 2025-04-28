using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using wellmanage.clientapp.Shared.Services;
using wellmanage.shared.Models;

namespace wellmanage.clientapp.Shared.Layout
{
    public partial class NavMenu
    {
        [Inject]
        AttendenceService attendenceService { get; set; }
        AttendanceStatus currentAttendenceStatus = new AttendanceStatus();

        protected override void OnInitialized()
        {
            attendenceService.OnAttendenceChanged += (attendenceStatus) =>
            {
                currentAttendenceStatus = attendenceStatus;
                StateHasChanged();
            };
        }
        public async Task CheckIn()
        {
           await attendenceService.CheckInUser();
        }
        public async Task CheckOut()
        {
            await attendenceService.CheckOutUser();
        }
    }
}
