using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class TasksL
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }
    }
}
