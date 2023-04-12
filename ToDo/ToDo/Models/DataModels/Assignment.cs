using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.Models.DataModels
{
    public class Assignment
    {
        public Assignment()
        {
        }

        public Assignment(int assignmentID, IdentityUser user, string assignmentName,
            string assignmentDescription, DateTime dateAssigned, DateTime dueDate, bool completed)
        {
            AssignmentID = assignmentID;
            User = user;
            AssignmentName = assignmentName;
            AssignmentDescription = assignmentDescription;
            DateAssigned = dateAssigned;
            DueDate = dueDate;
            Completed = completed;
        }

        public int AssignmentID { get; set; }
        public IdentityUser User { get; set; }
        [StringLength(90, ErrorMessage = "Assignment title is too long, cannot exceed 90 characters")]
        [Required]
        public string AssignmentName { get; set; }
        [StringLength(500, ErrorMessage = "Assignment title is too long, cannot exceed 90 characters")]
        [Required]
        public string AssignmentDescription { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateAssigned { get; set; } = DateTime.Now;
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }
        [Required]
        public Boolean Completed { get; set; } = false;
        [NotMapped]
        public string StrDate { get; set; }
        [NotMapped]
        public string StrTime { get; set; }


    }
}
