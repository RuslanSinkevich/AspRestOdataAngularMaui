using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        // [DataType(DataType.Date)]
        // public DateTime CreatedAt { get; set; }
        //

        public int StatusId { get; set; }


        public int TaskListId { get; set; }


    }
}
