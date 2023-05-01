using System.ComponentModel.DataAnnotations;
using ToDo.Interfaces;

namespace ToDo.ViewModels
{
    public class AssignmentEditViewModel : IAssignmentViewModel
    {
        public int AssignmentID { get; set; }
        public string AssignmentDescription { get; set; }
        public string AssignmentName { get; set; }
        public DateTime DueDate { get; set; }

        /*[DataType(DataType.DateTime)]
public DateTime? DateAssigned { get; set; } = DateTime.Now;*/

        public AssignmentEditViewModel()
        {

        }

        public AssignmentEditViewModel(int assignmentId, string assignmentName, string assignmentDescription, DateTime dueDate)
        {
            AssignmentID = assignmentId;
            AssignmentDescription = assignmentDescription;
            AssignmentName = assignmentName;
            DueDate = dueDate;
        }
    }
}
