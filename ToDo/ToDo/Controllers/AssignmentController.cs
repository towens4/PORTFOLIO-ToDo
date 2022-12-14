using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using ToDo.Interfaces;
using ToDo.Models.DataContexts;
using ToDo.Models.DataModels;
using ToDo.ViewModels;

namespace ToDo.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly IToDoRepository _repository;
        private readonly ToDoDataContext _toDoDataContext;
        private readonly UserManager<IdentityUser> _userManager;
        public AssignmentController(IToDoRepository repository, ToDoDataContext toDoDataContext, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _toDoDataContext = toDoDataContext;
            _userManager = userManager;
        }
        [Authorize]
        public IActionResult Index()
        {
            //var userID = _userManager.GetUserId(HttpContext.User);
            return View(_repository.GetAssignments(HttpContext.Session.GetString("Id")));
        }

        public IActionResult CreateAssignment()
        {
            return View(new AssignmentCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssignment(AssignmentCreateViewModel assignment)
        {
            if(!ModelState.IsValid)
                return View(assignment);

            var sessionResult = HttpContext.Session.GetString("Id");
            var userID = _userManager.GetUserId(HttpContext.User);

            //Debug.WriteLine(userID);

            Assignment newAssignment = new Assignment()
            {
                AssignmentDescription = assignment.AssignmentDescription,
                AssignmentName = assignment.AssignmentName,
                DueDate = assignment.DueDate,
                User = await _repository.getUserAsync(HttpContext.Session.GetString("Id"))
            };

            _toDoDataContext.Add(newAssignment);
            _toDoDataContext.SaveChanges();

            return RedirectToAction("Index", "Assignment");
        }

        [HttpGet("{id:int}")]
        //[Route("Assignment/EditAssignment/{id}")]
        public IActionResult EditAssignment(int id)
        {
            Assignment currentAssignment = _repository.GetById(id);
            AssignmentEditViewModel assignment = new AssignmentEditViewModel()
            {
                AssignmentID = currentAssignment.AssignmentID,
                AssignmentName = currentAssignment.AssignmentName,
                AssignmentDescription = currentAssignment.AssignmentDescription,
                DueDate = currentAssignment.DueDate,
            };

            return View(assignment);
        }

        [HttpPut]
        public IActionResult EditAssignment(AssignmentEditViewModel assignmentToEdit)
        {
            if (assignmentToEdit == null)
                return View(assignmentToEdit);

            if (!ModelState.IsValid)
            {
                return View(assignmentToEdit);
            }



            try
            {
                Assignment newAssignment = new Assignment()
                {
                    AssignmentID = assignmentToEdit.AssignmentID,
                    AssignmentDescription = assignmentToEdit.AssignmentDescription,
                    AssignmentName = assignmentToEdit.AssignmentName,
                    DueDate = assignmentToEdit.DueDate,
                };


                _toDoDataContext.Update(newAssignment);
                _toDoDataContext.SaveChanges();
            }
            catch(Exception exception)
            {
                Debug.WriteLine($"{exception.Message} {exception.StackTrace}");
            }

            

            return RedirectToAction("Index", "Assignment");
        }

        [HttpDelete]
        public IActionResult RemoveAssignment(int id)
        {
            _toDoDataContext.Remove(id);
            _toDoDataContext.SaveChanges();
            return RedirectToAction("Index", "Assignment");
        }
    }
}
