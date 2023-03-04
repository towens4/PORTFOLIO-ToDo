using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
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
        public IActionResult Index(int? id)
        {
            var userID = HttpContext.Session.GetString("Id");
            var assignmentList = _repository.GetAssignments(userID);
            Assignment? assignment = id == null? null: _repository.GetById((int)id);
            
            AssignmentListModel assignmentListModel = new AssignmentListModel()
            {
                AssignmentList = assignmentList,
                Assignment = assignment,
            };
            return View(assignmentListModel);
        }

        public IActionResult CreateAssignment()
        {
            return View(new AssignmentListModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssignment(AssignmentListModel assignmentListModel)
        {
            ModelState.Remove("AssignmentList");
            ModelState.Remove("Assignment");

            if (!ModelState.IsValid)
                return View(assignmentListModel);

            var sessionResult = HttpContext.Session.GetString("Id");
            var userID = _userManager.GetUserId(HttpContext.User);

            //Debug.WriteLine(userID);

            Assignment newAssignment = new Assignment()
            {
                AssignmentDescription = assignmentListModel.AssignmentEditViewModel.AssignmentDescription,
                AssignmentName = assignmentListModel.AssignmentEditViewModel.AssignmentName,
                DueDate = assignmentListModel.AssignmentEditViewModel.DueDate,
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
                AssignmentListModel assignmentListModel = new AssignmentListModel();
                assignmentListModel.AssignmentEditViewModel = new AssignmentEditViewModel();
                
                assignmentListModel.AssignmentEditViewModel.AssignmentID = model.AssignmentID;
                assignmentListModel.AssignmentEditViewModel.AssignmentName = model.AssignmentName;
                assignmentListModel.AssignmentEditViewModel.AssignmentDescription = model.AssignmentDescription;
                assignmentListModel.AssignmentEditViewModel.DueDate = model.DueDate;

                return View(assignmentListModel);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditAssignment(AssignmentListModel assignmentToEdit)
        {
            ModelState.Remove("AssignmentList");
            ModelState.Remove("Assignment");

            if (ModelState.IsValid)
            {
                try
                {
                    Assignment newAssignment = _repository.GetById(assignmentToEdit.AssignmentEditViewModel.AssignmentID);
                    newAssignment.AssignmentID = assignmentToEdit.AssignmentEditViewModel.AssignmentID;
                    newAssignment.AssignmentDescription = assignmentToEdit.AssignmentEditViewModel.AssignmentDescription;
                    newAssignment.AssignmentName = assignmentToEdit.AssignmentEditViewModel.AssignmentName;
                    newAssignment.DueDate = assignmentToEdit.AssignmentEditViewModel.DueDate;


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

        public IActionResult AssignmentDetails(int id)
        {
            Assignment model = _repository.GetById(id);
            AssignmentEditViewModel editModel = new AssignmentEditViewModel() 
            { 
                AssignmentDescription = model.AssignmentDescription,
                AssignmentName = model.AssignmentName,
                AssignmentID = model.AssignmentID,
                DueDate = model.DueDate,
            };
            return View(new AssignmentListModel() { AssignmentEditViewModel = editModel});
        }

        public IActionResult TaskList(int? id)
        {
            var userID = HttpContext.Session.GetString("Id");
            var assignmentList = _repository.GetAssignments(userID);
            Assignment? assignment = id == null ? null : _repository.GetById((int)id);

            AssignmentListModel assignmentListModel = new AssignmentListModel()
            {
                AssignmentList = assignmentList,
                Assignment = assignment,
            };

            return PartialView(assignmentListModel);
        }
    }
}
