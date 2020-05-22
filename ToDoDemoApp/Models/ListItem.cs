using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoDemoApp.Models
{
    public class ListItem
    {
        public int Id { get; set; }

        public DateTime AddDateTime { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Title is too short!")]
        [MaxLength(200, ErrorMessage = "Come up with a shorter title, please!")]
        public string Title { get; set; }

        public bool IsDone { get; set; }
    }
}
