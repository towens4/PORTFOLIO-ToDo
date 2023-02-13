﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDo.Interfaces;

namespace ToDo.ViewModels
{
    public class AssignmentDetailsViewModel : IAssignmentViewModel
    {
        //public int AssignmentID { get; set; }
        [StringLength(90, ErrorMessage = "Assignment title is too long, cannot exceed 90 characters")]
        [Required]
        public string AssignmentName { get; set; }
        [StringLength(500, ErrorMessage = "Assignment title is too long, cannot exceed 90 characters")]
        [Required]
        public string AssignmentDescription { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }
        public string StrDate { get; set; }
        public string StrTime { get; set; }
    }
}