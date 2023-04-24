using System.Diagnostics;
using System.Text;
using System.Text.Json;
using TodoREST.Models;
using TodoREST.Services.IServices;
using TodoREST.utility;

namespace TodoREST.Services
{
    public class TasksService : ITasksService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;

        public List<Tasks> Items { get; private set; }

        public TasksService(IHttpsClientHandlerService service)
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

        public async Task<List<Tasks>> GetTasksAsync(int tasksListId)
        {
            Items = new List<Tasks>();

            Uri uri = new Uri(string.Format(Constants.RestUrl + 
                                            "Tasks?$filter=taskListId eq " + 
                                            tasksListId +
                                            "&$orderby=statusId", string.Empty));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonSerializer.Deserialize<List<Tasks>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task RefreshTasksItemAsync(Tasks item, bool isNewItem)
        {
            Uri uri;

            try
            {
                string json = JsonSerializer.Serialize(item, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response;
                if (isNewItem)
                {
                    uri = new Uri(string.Format(Constants.RestUrl + "Tasks", string.Empty));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    uri = new Uri(string.Format(Constants.RestUrl + "Tasks?key=" + item.Id, string.Empty));
                    response = await _client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"\tTodoItem successfully saved.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task DeleteTasksItemAsync(Tasks item)
        {
            Uri uri = uri = new Uri(string.Format(Constants.RestUrl + "Tasks?key=" + item.Id, string.Empty));

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
