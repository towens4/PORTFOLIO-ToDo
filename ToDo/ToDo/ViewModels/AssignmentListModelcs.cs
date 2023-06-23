using Microsoft.AspNetCore.Identity;
using ToDo.Models.DataModels;

namespace ToDo.ViewModels
{
    public class AssignmentListModel
    {
        public IEnumerable<Assignment> AssignmentList { get; set; }
        public IEnumerable<Assignment> TotalAssisignments { get; set; }
        public Assignment? Assignment { get; set; }
        public AssignmentEditViewModel? AssignmentEditViewModel { get; set; }
        public LoginViewModel? LoginModel { get; set; }
        public RegisterViewModel? RegisterModel { get; set; }
    }
}
