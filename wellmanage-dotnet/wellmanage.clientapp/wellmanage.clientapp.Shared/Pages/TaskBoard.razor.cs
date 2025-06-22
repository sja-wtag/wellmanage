using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using wellmanage.clientapp.Shared.Components;
using wellmanage.clientapp.Shared.Services;
using wellmanage.shared.Enums;
using wellmanage.shared.Models;

namespace wellmanage.clientapp.Shared.Pages
{
    partial class TaskBoard
    {
        private MudDropContainer<KanbanTaskItem> _dropContainer;
        private bool _addSectionOpen;
        [Inject]
        private EmployeeService empService { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }
        [Inject]
        private TaskService taskService { get; set; }
        private List<ProjectTaskDto> tasks { get; set; }
        private List<KanbanTaskItem> _tasks = new() { };



        protected override void OnInitialized()
        {
            LoadUserTasks();
        }

        async Task LoadUserTasks()
        {
            _tasks = new() { };
            tasks = await empService.GetTasksAssignedToEmployee();
            tasks.ForEach(task =>
            {
                var section = new KanBanSections((long)task.TaskStatus, _statusDisplayNames[task.TaskStatus], false, task.Title, task.TaskId);
                AddTask(section);
            });

            await InvokeAsync(StateHasChanged);
        }

        private void TaskUpdated(MudItemDropInfo<KanbanTaskItem> info)
        {
            info.Item.Status = info.DropzoneIdentifier;
            var status = _statusDisplayNames.FirstOrDefault(status => status.Value == info.DropzoneIdentifier).Key;
            taskService.UpdateTaskStatusAsync(info.Item.TaskId, status);
        }

        private static readonly Dictionary<TaskStatusEnum, string> _statusDisplayNames = new()
{
    { TaskStatusEnum.Todo, "To Do" },
    { TaskStatusEnum.InProgress, "In Progress" },
    { TaskStatusEnum.QA, "QA" },
    { TaskStatusEnum.ReadyForProd, "Ready For Prod" },
    { TaskStatusEnum.Done, "Done" }
};

        private List<KanBanSections> _sections = Enum.GetValues(typeof(TaskStatusEnum))
            .Cast<TaskStatusEnum>()
            .Select(status => new KanBanSections(
                (long)status,
                _statusDisplayNames[status],
                false,
                string.Empty, 0))
            .ToList();


        public class KanBanSections
        {
            public long SectionId { get; set; }
            public string Name { get; init; }
            public bool NewTaskOpen { get; set; }
            public string NewTaskName { get; set; }
            public long NewTaskId { get; set; }

            public KanBanSections(long Id, string name, bool newTaskOpen, string newTaskName, long newTaskId)
            {
                SectionId = Id;
                Name = name;
                NewTaskOpen = newTaskOpen;
                NewTaskName = newTaskName;
                NewTaskId = newTaskId;
            }
        }

        public class KanbanTaskItem
        {
            public long TaskId { get; set; }
            public string Name { get; init; }
            public string Status { get; set; }

            public KanbanTaskItem(long taskId, string name, string status)
            {
                TaskId = taskId;
                Name = name;
                Status = status;
            }
        }


        KanBanNewForm newSectionModel = new KanBanNewForm();

        public class KanBanNewForm
        {
            [Required]
            [StringLength(10, ErrorMessage = "Name length can't be more than 10.")]
            public string Name { get; set; }
        }

        private void OnValidSectionSubmit(EditContext context)
        {
            _sections.Add(new KanBanSections(0, newSectionModel.Name, false, string.Empty, 0));
            newSectionModel.Name = string.Empty;
            _addSectionOpen = false;
        }

        private void OpenAddNewSection()
        {
            _addSectionOpen = true;
        }

        private void AddTask(KanBanSections section)
        {
            if (!string.IsNullOrWhiteSpace(section.NewTaskName))
            {
                _tasks.Add(new KanbanTaskItem(section.NewTaskId, section.NewTaskName, section.Name));
                section.NewTaskName = string.Empty;
                section.NewTaskOpen = false;
                _dropContainer?.Refresh();
            }
        }

        private void DeleteSection(KanBanSections section)
        {
            if (_sections.Count == 1)
            {
                _tasks.Clear();
                _sections.Clear();
            }
            else
            {
                int newIndex = _sections.IndexOf(section) - 1;
                if (newIndex < 0) newIndex = 0;

                _sections.Remove(section);

                var tasks = _tasks.Where(x => x.Status == section.Name).ToList();
                foreach (var item in tasks)
                {
                    item.Status = _sections[newIndex].Name;
                }
            }
        }

        private KanbanTaskItem? _selectedTask;
        private bool _isTaskModalOpen = false;

        private void OpenTaskModal(KanbanTaskItem task)
        {
            _selectedTask = task;
            _isTaskModalOpen = true;
        }
        private void OnModalVisibilityChanged(bool saveAction)
        {
            _isTaskModalOpen = false;
            if (saveAction)
            {
                LoadUserTasks();
            }
        }

        private void GoToCreateNewTask()
        {
            navigationManager.NavigateTo("create-task");
        }
        private void CloseTaskModal()
        {
            _isTaskModalOpen = false;
            _selectedTask = null;
        }
    }
}
