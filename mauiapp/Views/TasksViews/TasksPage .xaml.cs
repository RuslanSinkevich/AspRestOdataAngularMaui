using TodoREST.Models;
using TodoREST.Services.IServices;
using TodoREST.utility;

namespace TodoREST.Views.TasksViews
{
    [QueryProperty(nameof(TasksList), "TasksList")]
    public partial class TasksPage : ContentPage
    {
        
        private readonly ITasksService _todoService;
        TasksList _tasksList;
        Tasks _tasks;

        public Tasks Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged();
            }
        }

        public TasksList TasksList
        {
            get => _tasksList;
            set
            {
                _tasksList = value;
                OnPropertyChanged();
            }
        }

        public TasksPage(ITasksService service)
        {
            InitializeComponent();
            _todoService = service;
            BindingContext = this;

        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();
            if (TasksList != null)
            {
                TasksView.ItemsSource = await _todoService.GetTasksAsync(TasksList.Id);
            }

            BindingContext = this;
        }

        bool IsNewItem(Tasks tasks)
        {
            if (string.IsNullOrWhiteSpace(tasks.Title) )
                return true;
            return false;
        }

        async void OnAddItemClicked(object sender, EventArgs e)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { nameof(Tasks), new Tasks
                {
                    Id = Utility.GetUniqueId(),
                    statusId = 1,
                    taskListId = TasksList.Id
                } }
            };
            await Shell.Current.GoToAsync(nameof(AddTaskPage), navigationParameter);
        }
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if ((sender as Button)?.CommandParameter is Tasks item)
            {
                item.statusId = item.statusId == 1 ? 2 : 1;
                await _todoService.RefreshTasksItemAsync(item, false);
                TasksView.ItemsSource = await _todoService.GetTasksAsync(TasksList.Id);

            }
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if ((sender as Button)?.CommandParameter is Tasks item)
            {
                await _todoService.DeleteTasksItemAsync(item);
                TasksView.ItemsSource = await _todoService.GetTasksAsync(TasksList.Id);
            }
        }

        async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
