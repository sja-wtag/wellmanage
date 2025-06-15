using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoMapper.Execution;
using AutoMapper.Internal;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.Ocsp;
using wellmanage.application.Interfaces;
using wellmanage.domain.Entity;
using wellmanage.hrm.client.Service_Container;
using wellmanage.shared.Enums;
using wellmanage.shared.Models;

namespace wellmanage.hrm.client
{
    public partial class EmployeeUpdateForm : Form
    {
        private readonly IEmployeeService _employeeService;
        private readonly IProjectService _projectService;
        private List<EmployeeDto> members = new List<EmployeeDto>();
        private List<ProjectDto> projects = new List<ProjectDto>();
        EmployeeDto selectedEmployee = null;
        EmployeeDto defaultItem = new EmployeeDto(null, "Select");
        private UserInfo selectedUser;

        public EmployeeUpdateForm(UserInfo selectedUser)
        {
            _employeeService = ServiceContainer.Services.GetRequiredService<IEmployeeService>();
            _projectService = ServiceContainer.Services.GetRequiredService<IProjectService>();
            this.selectedUser = selectedUser;
            InitData();

        }

        public EmployeeUpdateForm(EmployeeDto employeeDto)
        {
            _employeeService = ServiceContainer.Services.GetRequiredService<IEmployeeService>();
            _projectService = ServiceContainer.Services.GetRequiredService<IProjectService>();
            this.selectedUser = employeeDto.User;
            this.selectedEmployee = employeeDto;
            InitData();
        }

        public async Task InitData()
        {
            InitializeComponent();
            await GetMembers();
            LoadDesignations();
            LoadDepartments();
            await LoadProjects();
            comboBox3.DataSource = Enum.GetValues(typeof(StatusEnum));
            comboBox3.SelectedItem = selectedUser.Status;
        }

        public async Task GetMembers()
        {
            try
            {
                var employees = await _employeeService.GetEmployeesWithUserInformation();
                members = employees.Where(item => item.UserId != selectedUser.Id).ToList();
                teamLeadCombobox.DataSource = members;
                teamLeadCombobox.SelectedItem = members.Find(item => item.Id == selectedEmployee?.TeamLeadId);
                foreach (var member in members)
                {
                    int index = assigniesListBox.Items.Add(member);

                    if (selectedEmployee?.Assignees?.Any(a => a.Id == member.Id) == true)
                    {
                        assigniesListBox.SetItemChecked(index, true);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var request = new EmployeeSaveRequest()
            {
                UserId = selectedUser.Id,
                Department = departmentCombobox.SelectedItem?.ToString(),
                JoiningDate = joiningDateTimePicker.Value,
                Designation = designationCombobox.SelectedItem?.ToString(),
                TeamLeadId = (teamLeadCombobox.SelectedValue as EmployeeDto)?.Id,
                Assignies = GetSelectedAssignies(),
                Projects = GetSelectedProjects()
            };

            var errorMsg = ValidateSubmission(request);
            if (errorMsg != null)
            {
                MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                AddEmployee(request);
                this.Hide();
            }
        }

        private async Task AddEmployee(EmployeeSaveRequest request)
        {
            await _employeeService.AddEmployee(request);
            MessageBox.Show("Employee Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoadDesignations()
        {
            // Create a list of designations
            List<string> designations = new List<string>
            {
                "Software Engineer",
                "Senior Software Engineer",
                "Team Lead",
                "Project Manager",
                "QA Engineer",
                "Business Analyst",
                "Product Manager",
                "HR Manager",
                "CTO",
                "CEO"
            };

            // Bind the list to the ComboBox
            designationCombobox.DataSource = designations;
        }
        private void LoadDepartments()
        {
            List<string> departments = new List<string>
            {
                "Engineering",
                "Human Resources",
                "Finance",
                "Marketing",
                "Sales",
                "Customer Support",
                "Operations",
                "IT",
                "Legal",
                "Research and Development"
            };

            // Bind the list to the ComboBox
            departmentCombobox.DataSource = departments;
        }

        private async Task LoadProjects()
        {
            projects = (List<ProjectDto>)await _projectService.GetAllProjectsAsync();
            projectListBox.Items.Clear();

            foreach (var project in projects)
            {
                int index = projectListBox.Items.Add(project);
                if (selectedEmployee?.Projects?.Any(item => item.Id == project.Id) == true)
                {
                    projectListBox.SetItemChecked(index, true);
                }
            }
        }



        private string ValidateSubmission(EmployeeSaveRequest request)
        {
            string errorMessage = null;
            if (string.IsNullOrEmpty(request.Designation))
            {
                return "Please Select Designation";
            }
            else if (request.JoiningDate == null)
            {
                return "Please Select Joining Date";
            }
            else if (request.TeamLeadId.HasValue && request.Assignies.Contains(request.TeamLeadId.Value))
            {
                return "Please Remove Team Lead From Assignies";
            }

            return errorMessage;
        }

        private List<long> GetSelectedAssignies()
        {
            var selectedAssignies = new List<long>();
            foreach (var item in assigniesListBox.CheckedItems)
            {
                if (item is EmployeeDto employee)
                {
                    selectedAssignies.Add(employee.Id.Value);
                }
            }

            return selectedAssignies;
        }

        private List<long> GetSelectedProjects()
        {
            var selectedProjects = new List<long>();
            foreach (var item in projectListBox.CheckedItems)
            {
                if (item is ProjectDto project)
                {
                    selectedProjects.Add(project.Id);
                }
            }

            return selectedProjects;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
