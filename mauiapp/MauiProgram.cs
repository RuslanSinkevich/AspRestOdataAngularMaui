using TodoREST.Services;
using TodoREST.Services.IServices;
using TodoREST.Views;
using TodoREST.Views.TasksListViews;
using TodoREST.Views.TasksViews;


namespace TodoREST;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<IHttpsClientHandlerService, HttpsClientHandlerService>();
        builder.Services.AddSingleton<ITasksListService, TasksListService>();
        builder.Services.AddSingleton<ITasksService, TasksService>();

        builder.Services.AddSingleton<TasksListPage>();
		builder.Services.AddTransient<TasksPage>();
		builder.Services.AddTransient<AddListPage>();
		builder.Services.AddTransient<AddTaskPage>();

        return builder.Build();
	}
}
