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
            //var assignmentList = _repository.GetAssignmentsAsync(userID);
            IEnumerable<Assignment> assignments = _repository.GetAssignments(userID);
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                assignments =  new List<Assignment>();
            }

            AssignmentListModel assignmentListModel = new AssignmentListModel() 
            { 
                AssignmentList = assignments
            };

            
            return View(assignmentListModel);
        }
    }
}
