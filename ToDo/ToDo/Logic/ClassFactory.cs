using ToDo.Models.DataModels;
using ToDo.ViewModels;

namespace ToDo.Logic
{
    public class ClassFactory
    {
        public static AssignmentEditViewModel CreateAssignmentEditViewModel(Assignment model)
        {
            return new AssignmentEditViewModel()
            {
                AssignmentID = model.AssignmentID,
                AssignmentName = model.AssignmentName,
                AssignmentDescription = model.AssignmentDescription,
                DueDate = model.DueDate,
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
