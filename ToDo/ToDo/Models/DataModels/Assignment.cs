using Microsoft.AspNetCore.Identity;

namespace ToDo.Models.DataModels
{
    public class Assignment
    {
        public int AssignmentID { get; set; }
        public IdentityUser UserID { get; set; }
        public string AssignmentName { get; set; }
        public string AssignmentDescription { get; set; }
        public DateTime DateAssigned { get; set; }
        public DateTime DueDate { get; set; }
        public int Completed { get; set; }
    }
}
