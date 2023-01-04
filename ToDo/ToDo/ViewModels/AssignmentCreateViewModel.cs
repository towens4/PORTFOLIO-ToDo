using System.ComponentModel.DataAnnotations;

namespace ToDo.ViewModels
{
    public class AssignmentCreateViewModel
    {
        //public string UserId { get; set; }
        [StringLength(90, ErrorMessage = "Assignment title is too long, cannot exceed 90 characters")]
        [Required]
        public string AssignmentName { get; set; }
        [StringLength(500, ErrorMessage = "Assignment title is too long, cannot exceed 90 characters")]
        [Required]
        public string AssignmentDescription { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }
        
    }
}
