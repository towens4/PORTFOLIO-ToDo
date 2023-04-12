using Microsoft.VisualBasic;
using ToDo.Models.DataModels;
using ToDo.ViewModels;

namespace ToDo.Logic
{
    public class ClassFactory
    {
        public static AssignmentEditViewModel CreateAssignmentEditViewModel(int assignmentId, string assignmentName, 
            string assignmentDescription, DateTime dueDate)
        {
            return new AssignmentEditViewModel()
            {
                AssignmentID = assignmentId,
                AssignmentName = assignmentName,
                AssignmentDescription = assignmentDescription,
                DueDate = dueDate,
            };
        }

        public static AssignmentCreateViewModel CreateAssignmentCreateViewModel()
        {
            return new AssignmentCreateViewModel() { DueDate = DateTime.Now};
        }

        public static AssignmentListModel CreateAssignmentListModel(List<Assignment> assignments, Assignment assignment)
        {
            return new AssignmentListModel() { AssignmentList = assignments, Assignment = assignment };
        }
    }
}
