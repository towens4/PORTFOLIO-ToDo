using AutoMapper;
using Azure.Core;
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
        private readonly ILogger<AssignmentController> _logger;
        //private readonly ToDoDataContext _toDoDataContext;
        
        public AssignmentController(IToDoRepository repository, ILogger<AssignmentController> logger)
        {
            _repository = repository;
            _logger = logger;
            //_toDoDataContext = toDoDataContext;
        }
        [Authorize]
        public IActionResult Index()
        {
            /*var userID = HttpContext.Session.GetString("Id");
            var assignmentList = _repository.GetAssignments(userID);
            Assignment? assignment = id == null? null: _repository.GetById((int)id);
            
            AssignmentListModel assignmentListModel = new AssignmentListModel()
            {
                AssignmentList = assignmentList,
                Assignment = assignment,
            };*/

            /*_logger.LogTrace("This is a Trace Log");
            _logger.LogDebug("This is a debug log");
            _logger.LogInformation("This is our first logging message.");
            _logger.LogWarning("This is a warning log");
            _logger.LogError("This is an error log");
            _logger.LogCritical("This is a critical log");*/

            

            return View();
        }

        [HttpGet]
        public IActionResult CreateAssignment()
        {
            DateTime dueDateInitialize = DateTime.Now;
            
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView(new AssignmentCreateViewModel() { DueDate = dueDateInitialize});
            return View(new AssignmentCreateViewModel());
        }

        /*
         * TODO: Separate the DateTime value into a two variables of date and time.
         * 
         * 
         */
        [HttpPost]
        public async Task<IActionResult> CreateAssignment([FromBody]AssignmentCreateViewModel model)
        {
            Response.Headers.Add("is-valid", "");
            
            if (!ModelState.IsValid)
            {
                
                if(Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    Response.Headers["is-valid"] = "false";
                    return PartialView(model);
                }
                    
                return View(model);
            }

            Response.Headers["is-valid"] = "true";

            //string dateTime = model.Date + " " + model.Time;

            Assignment newAssignment = new Assignment()
            {
                AssignmentDescription = model.AssignmentDescription,
                AssignmentName = model.AssignmentName,
                DueDate = model.DueDate,
                User = await _repository.getUserAsync(HttpContext.Session.GetString("Id"))
            };

            _repository.CreateAssignment(newAssignment);

            return RedirectToAction("Index", "Assignment");
        }

        [HttpGet] 
        [Route("Assignment/EditAssignment")]
        public IActionResult EditAssignment(int id)
        {
            try
            {
                Assignment model = _repository.GetById(id);
                AssignmentEditViewModel assignmentEditViewModel =
                    new AssignmentEditViewModel(model.AssignmentID, model.AssignmentName, model.AssignmentDescription, model.DueDate);
                

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    Response.Headers["is-valid"] = "false";
                    return PartialView(assignmentEditViewModel);
                }
                return View(assignmentEditViewModel);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditAssignment([FromBody]AssignmentEditViewModel assignmentToEdit)
        {

            Response.Headers.Add("is-valid", "");
            if (ModelState.IsValid)
            {
                try
                {
                    Assignment newAssignment = _repository.GetById(assignmentToEdit.AssignmentID);
                    newAssignment.AssignmentID = assignmentToEdit.AssignmentID;
                    newAssignment.AssignmentDescription = assignmentToEdit.AssignmentDescription;
                    newAssignment.AssignmentName = assignmentToEdit.AssignmentName;
                    newAssignment.DueDate = assignmentToEdit.DueDate;
                    Response.Headers["is-valid"] = "true";

                    _repository.UpdateAssignment(newAssignment);
                    return RedirectToAction("Index", "Assignment");
                }
                catch (Exception exception)
                {
                    _logger.LogWarning(exception, "There was a bad exception at [Time]", DateTime.UtcNow);
                    Debug.WriteLine($"{exception.Message} {exception.StackTrace}");
                }
                
            }
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                Response.Headers["is-valid"] = "false";
                return PartialView(assignmentToEdit);
            }
            return View(assignmentToEdit);
        }

        //[HttpDelete]
        public IActionResult RemoveAssignment(int id)
        {
            Assignment assignment = _repository.GetById(id);
            _repository.DeleteAssignment(assignment);
            return RedirectToAction("Index", "Assignment");
        }

        public async Task<IActionResult> AssignmentDetails(int id)
        {
            Assignment model = _repository.GetById(id);
            AssignmentEditViewModel editModel = new AssignmentEditViewModel(model.AssignmentID, model.AssignmentName, 
                model.AssignmentDescription, model.DueDate);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView(editModel);

            return View(editModel);
        }

        
    }
}
