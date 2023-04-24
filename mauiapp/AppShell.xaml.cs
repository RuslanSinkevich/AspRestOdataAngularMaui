using TodoREST.Views.TasksListViews;
using TodoREST.Views.TasksViews;

namespace TodoREST;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(AddListPage), typeof(AddListPage));
		Routing.RegisterRoute(nameof(TasksPage), typeof(TasksPage));
		Routing.RegisterRoute(nameof(AddTaskPage), typeof(AddTaskPage));
    }
}
