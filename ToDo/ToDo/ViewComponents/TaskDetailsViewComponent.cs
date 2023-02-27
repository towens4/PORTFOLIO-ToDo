using Microsoft.AspNetCore.Mvc;
using ToDo.Interfaces;
using ToDo.Logic;

namespace ToDo.ViewComponents
{
    public class TaskDetailsViewComponent : ViewComponent
    {
        private readonly IToDoRepository _repository;
        public TaskDetailsViewComponent(IToDoRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke(int id)
        {
            var userId = HttpContext.Session.GetString("Id");

            return View(ClassFactory.CreateAssignmentListModel(
                _repository.GetAssignments(userId), _repository.GetById(id)));
        }
    }
}
