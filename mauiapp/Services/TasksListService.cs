using System.Diagnostics;
using System.Text;
using System.Text.Json;
using TodoREST.Models;
using TodoREST.Services.IServices;
using TodoREST.utility;

namespace TodoREST.Services
{
    public class TasksListService : ITasksListService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;

        public List<TasksList> Items { get; private set; }

        public TasksListService(IHttpsClientHandlerService service)
        {
#if DEBUG
            var handler = service.GetPlatformMessageHandler();
            _client = handler != null ? new HttpClient(handler) : new HttpClient();
#else
            _client = new HttpClient();
#endif
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<TasksList>> GetTasksListAsync()
        {
            Items = new List<TasksList>();

            Uri uri = new Uri(string.Format(Constants.RestUrl + "ListTasks", string.Empty));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonSerializer.Deserialize<List<TasksList>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task RefreshTasksListItemAsync(TasksList item)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl + "ListTasks", string.Empty));

            try
            {
                string json = JsonSerializer.Serialize(item, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                    response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"\tTodoItem successfully saved.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task DeleteTasksListItemAsync(TasksList item)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl + "ListTasks?key=" + item.Id, string.Empty));

            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"\tTodoItem successfully deleted.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}
