using System.ComponentModel.DataAnnotations;

namespace ToDo.ViewModels
{
    public class AssignmentEditViewModel : AssignmentCreateViewModel
    {
        public int AssignmentID { get; set; }
        /*[DataType(DataType.DateTime)]
        public DateTime? DateAssigned { get; set; } = DateTime.Now;*/
    }
}
