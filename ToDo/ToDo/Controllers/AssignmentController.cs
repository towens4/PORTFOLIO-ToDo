using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using ToDo.Interfaces;
using ToDo.Logic;
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
            var id = HttpContext.Session.GetString("Id");
            var assignmentList = _repository.GetAssignments(id);
            
            return View(assignmentList.ToList());
        }

        public IActionResult CreateAssignment()
        {
            return View(new AssignmentCreateViewModel() { DueDate = DateTime.Now});
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

        [HttpGet] 
        [Route("Assignment/EditAssignment")]
        public IActionResult EditAssignment(int id)
        {
            try
            {
                Assignment model = _repository.GetById(id);
                AssignmentEditViewModel assignmentEditViewModel = new AssignmentEditViewModel()
                {
                    AssignmentID = model.AssignmentID,
                    AssignmentName = model.AssignmentName,
                    AssignmentDescription = model.AssignmentDescription,
                    DueDate = model.DueDate,
                };

                return View(assignmentEditViewModel);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditAssignment(AssignmentEditViewModel assignmentToEdit)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Assignment newAssignment = _repository.GetById(assignmentToEdit.AssignmentID);
                    newAssignment.AssignmentID = assignmentToEdit.AssignmentID;
                    newAssignment.AssignmentDescription = assignmentToEdit.AssignmentDescription;
                    newAssignment.AssignmentName = assignmentToEdit.AssignmentName;
                    newAssignment.DueDate = assignmentToEdit.DueDate;


                    _toDoDataContext.Update(newAssignment);
                    _toDoDataContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Debug.WriteLine($"{exception.Message} {exception.StackTrace}");
                }
                return RedirectToAction("Index", "Assignment");
            }

          

            

            
            return View(assignmentToEdit);
        }

        //[HttpDelete]
        public IActionResult RemoveAssignment(int id)
        {
            Assignment assignment = _repository.GetById(id);
            _toDoDataContext.Assignments.Remove(assignment);
            _toDoDataContext.SaveChanges();
            return RedirectToAction("Index", "Assignment");
        }
    }
}
