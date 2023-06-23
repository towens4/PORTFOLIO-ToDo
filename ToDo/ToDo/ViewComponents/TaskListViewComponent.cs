using Microsoft.AspNetCore.Mvc;
using System.Collections;
using ToDo.Interfaces;
using ToDo.Models.DataModels;
using ToDo.ViewModels;

namespace ToDo.ViewComponents
{
    public class TaskListViewComponent : ViewComponent
    {
        private readonly IToDoRepository _repository;
        public TaskListViewComponent(IToDoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userID = HttpContext.Session.GetString("Id");
            
            IEnumerable<Assignment> assignments = _repository.GetUncompletedAssignments(userID);
            IEnumerable<Assignment> allAssignments = _repository.GetAssignments(userID);
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                assignments =  new List<Assignment>();
            }

            if (Request.Headers["completed-flag"] == "true")
            {
                assignments = _repository.GetCompletedAssignments(userID);
            }

            AssignmentListModel assignmentListModel = new AssignmentListModel()
            {
                AssignmentList = assignments,
                TotalAssisignments = allAssignments
                
            };

            
            return View(assignmentListModel);
        }
    }
}
