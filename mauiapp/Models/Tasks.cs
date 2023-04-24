namespace TodoREST.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int statusId { get; set; }
        public int taskListId { get; set; }
    }
}
