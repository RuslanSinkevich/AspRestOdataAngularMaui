using TodoREST.Models;
using TodoREST.Services.IServices;

namespace TodoREST.Views.TasksViews
{
    [QueryProperty(nameof(Tasks), "Tasks")]
    public partial class AddTaskPage : ContentPage
    {
        readonly ITasksService _tasksService;
        Tasks _tasks;

        public Models.Tasks Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged();
            }
        }

        public AddTaskPage(ITasksService tasksService)
        {
            InitializeComponent();
            _tasksService = tasksService;
            BindingContext = this;
        }

        async void OnAddButtonClicked(object sender, EventArgs e)
        {
            await _tasksService.RefreshTasksItemAsync(Tasks, true);
            await Shell.Current.GoToAsync("..");
        }


        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            await _tasksService.DeleteTasksItemAsync(Tasks);
            await Shell.Current.GoToAsync("..");
        }

        async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
