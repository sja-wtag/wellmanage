using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using wellmanage.application.Interfaces;
using wellmanage.application.Services;
using wellmanage.hrm.client.Service_Container;
using wellmanage.shared.Models;

namespace wellmanage.hrm.client
{
    public partial class HomeForm : Form
    {
        private readonly IUserService _userService;
        private readonly IEmployeeService _employeeService;
        private UserInfo? userInfo = null;
        private List<EmployeeDto> members = new List<EmployeeDto>();
        private List<AttendanceDto> attendances = new List<AttendanceDto>();
        private long onboardingCount = 0;
        public HomeForm(IUserService userService)   
        {
            InitializeComponent();
            _userService = userService;
            _employeeService = ServiceContainer.Services.GetRequiredService<IEmployeeService>();
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            LoadAllData();
        }

        private async Task LoadAllData()
        {
            await GetUsersForOnboarding();
            await GetMembers();
            await GetAttendencesToday();
        }

        private async Task GetUsersForOnboarding()
        {
            var users = await _userService.GetUsersForOnboarding();

            onboardingGridData.DataSource = users;
            onboardingCount = users.Count();
            SetOnboardingCountText();
            foreach (DataGridViewColumn column in onboardingGridData.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.FillWeight = 1;
            }
        }

        private void SetOnboardingCountText()
        {
            label4.Text = $"{onboardingCount.ToString()} Pending";
        }

        private async Task GetMembers()
        {
            members = await _employeeService.GetEmployeesWithUserInformation();
            membersGridData.DataSource = members;
            foreach (DataGridViewColumn column in membersGridData.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.FillWeight = 1;
            }
        }

        private async Task GetAttendencesToday()
        {
            attendances = await _userService.GetAttendencesToday();
            attendancesGrid.DataSource = attendances;
            foreach (DataGridViewColumn column in attendancesGrid.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.FillWeight = 1; 
            }
        }

        private void AttendancesGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is DateTime utcTime)
            {
                DateTime localTime = utcTime.ToLocalTime();
                e.Value = localTime.ToString("g");
                e.FormattingApplied = true;

               
                if (attendancesGrid.Columns[e.ColumnIndex].Name == "CheckInTime")
                {
                    var lateTime = new TimeSpan(9, 30, 0); // 9:30 AM
                    if (localTime.TimeOfDay > lateTime)
                    {
                        e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.ForeColor = Color.White;
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.White;
                        e.CellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }



        private void onBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.onBoardingPanel.BringToFront();
        }

        private void HomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.homePanel.BringToFront();
        }


        private void MembersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.membersPanel.BringToFront();
        }

        private void AttendencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.attendencePanel.BringToFront();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Onboard_Click(object sender, EventArgs e)
        {
            if (userInfo == null)
            {
                MessageBox.Show("Please Select One User.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EmployeeUpdateForm empform = new EmployeeUpdateForm(userInfo);
            empform.Show();
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            userInfo = onboardingGridData.CurrentRow?.DataBoundItem as UserInfo;
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
