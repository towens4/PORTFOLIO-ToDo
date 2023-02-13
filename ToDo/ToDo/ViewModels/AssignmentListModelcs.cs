using ToDo.Models.DataModels;

namespace ToDo.ViewModels
{
    public class AssignmentListModel
    {
        public IEnumerable<Assignment> AssignmentList { get; set; }
        public  Assignment? Assignment { get; set; }
    }
}
