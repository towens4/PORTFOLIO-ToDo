using System.ComponentModel.DataAnnotations;
using ToDo.Interfaces;

namespace ToDo.ViewModels
{
    public class AssignmentCreateViewModel : IAssignmentViewModel
    {
        //public string UserId { get; set; }
        [Display(Name = "Assignment Name")]
        [StringLength(90, ErrorMessage = "Assignment title is too long, cannot exceed 90 characters")]
        [Required]
        public string AssignmentName { get; set; }
        [Display(Name = "Assignment Description")]
        [StringLength(500, ErrorMessage = "Assignment title is too long, cannot exceed 90 characters")]
        [Required]
        public string AssignmentDescription { get; set; }
        [Display(Name = "Due Date")]
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }
        //public bool Completed { get; set; } = false;
    }
}
