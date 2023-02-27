using Microsoft.AspNetCore.Mvc;
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
            var assignmentList = _repository.GetAssignments(userID);

            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                assignmentList = new List<Assignment>();
            }

            AssignmentListModel assignmentListModel = new AssignmentListModel() 
            { 
                AssignmentList = assignmentList
            };

            
            return View(assignmentListModel);
        }
    }
}
