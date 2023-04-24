using TodoREST.Models;
using TodoREST.Services.IServices;
using TodoREST.utility;

namespace TodoREST.Views.TasksListViews
{
    [QueryProperty(nameof(TasksList), "TasksList")]
    public partial class AddListPage : ContentPage
    {
        readonly ITasksListService _tasksListService;
        TasksList _tasksList;

        public TasksList TasksList
        {
            get => _tasksList;
            set
            {
                _tasksList = value;
                OnPropertyChanged();
            }
        }

        public AddListPage(ITasksListService tasksListService)
        {
            InitializeComponent();
            _tasksListService = tasksListService;
            BindingContext = this;
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            await _tasksListService.RefreshTasksListItemAsync(TasksList);
            await Shell.Current.GoToAsync("..");
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            await _tasksListService.DeleteTasksListItemAsync(TasksList);
            await Shell.Current.GoToAsync("..");
        }

        async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
