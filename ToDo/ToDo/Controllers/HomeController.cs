using Microsoft.AspNetCore.Mvc;
using ToDo.Interfaces;
using ToDo.Models.DataContexts;

namespace ToDo.Controllers
{
    public class HomeController : Controller
    {
        IToDoRepository _toDoDataRespository;
        
        public HomeController(IToDoRepository toDoDataRespository)
        {
            _toDoDataRespository = toDoDataRespository;
            
        }
        public IActionResult Index()
        {
            
            var list = _toDoDataRespository.GetAssignments(HttpContext.Session.Id);
            return View();
        }
    }
}
