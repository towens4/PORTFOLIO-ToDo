using Microsoft.AspNetCore.Mvc;
using ToDo.Models.DataContexts;

namespace ToDo.Controllers
{
    public class HomeController : Controller
    {
        ToDoDataRespository _toDoDataRespository;
        HttpClient _httpClient;
        public HomeController(ToDoDataRespository toDoDataRespository, HttpClient httpClient)
        {
            _toDoDataRespository = toDoDataRespository;
            _httpClient = httpClient;
        }
        public IActionResult Index()
        {
            
            return View(_toDoDataRespository.GetAssignments(HttpContext.Session.Id));
        }
    }
}
