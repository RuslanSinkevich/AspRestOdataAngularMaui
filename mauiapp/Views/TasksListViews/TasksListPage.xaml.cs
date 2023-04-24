using Microsoft.Maui.Controls;
using TodoREST.Models;
using TodoREST.Services.IServices;
using TodoREST.utility;
using TodoREST.Views.TasksViews;

namespace TodoREST.Views.TasksListViews
{
    public partial class TasksListPage : ContentPage
    {
        private readonly ITasksListService _tasksListService;
        public List<TasksList> TasksL { get; set; }

        public TasksListPage(ITasksListService tasksListService)
        {
            _tasksListService = tasksListService;
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();
            listView.ItemsSource = await _tasksListService.GetTasksListAsync();
            BindingContext = this;
        }


        async void OnAddItemClicked(object sender, EventArgs e)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { nameof(TasksList), new TasksList { Id = Utility.GetUniqueId()} }
            };
            await Shell.Current.GoToAsync(nameof(AddListPage), navigationParameter);
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { nameof(TasksList), e.CurrentSelection.FirstOrDefault() as TasksList }
            };
            await Shell.Current.GoToAsync(nameof(TasksPage), navigationParameter);
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if ((sender as Button)?.CommandParameter is TasksList item)
            {
                await Task.Run(() => _tasksListService.DeleteTasksListItemAsync(item));
            }
            listView.ItemsSource = await _tasksListService.GetTasksListAsync();

        }

    }

}
