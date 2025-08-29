
using System.ComponentModel.DataAnnotations;
using DeptSample.Data.Attributes;

namespace DeptSample.Data.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Date and time are required")]
        [FutureDate(ErrorMessage = "Reminder time must be in the future")]
        [Display(Name = "Reminder Date & Time")]
        public DateTime ReminderTime { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public bool IsSent { get; set; } = false;
    }
}
