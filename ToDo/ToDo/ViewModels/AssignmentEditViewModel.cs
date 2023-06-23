using System.ComponentModel.DataAnnotations;
using ToDo.Interfaces;

namespace ToDo.ViewModels
{
    public class AssignmentEditViewModel : IAssignmentViewModel
    {
        public int AssignmentID { get; set; }
        [Display(Name = "Assignment Description")]
        [Required]
        public string AssignmentDescription { get; set; }
        [Display(Name = "Assignment Name")]
        [Required]
        public string AssignmentName { get; set; }
        [Display(Name = "Due Date")]
        [Required]
        public DateTime DueDate { get; set; }
        public bool Completed { get; set; }

        public AssignmentEditViewModel()
        {

        }

        public AssignmentEditViewModel(int assignmentId, string assignmentName, string assignmentDescription, DateTime dueDate, bool completed)
        {
            AssignmentID = assignmentId;
            AssignmentDescription = assignmentDescription;
            AssignmentName = assignmentName;
            DueDate = dueDate;
            Completed = completed;
        }
    }
}
