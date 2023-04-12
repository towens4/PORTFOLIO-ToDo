using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ToDo.Interfaces;
using ToDo.Models.DataModels;

namespace ToDo.Models.DataContexts
{
    public class ToDoDataRespository : IToDoRepository
    {
        public static ToDoDataContext _toDoDataContext;
        private UserManager<IdentityUser> _userManager { get; set; }

        public ToDoDataRespository()
        {
        }

        public ToDoDataRespository(ToDoDataContext toDoDataContext, UserManager<IdentityUser> userManager)
        {
            _toDoDataContext = toDoDataContext;
            _userManager = userManager;
        }

        public List<Assignment> GetAssignments(string userId)
        {

            DateTime time;
            List<Assignment> listCompare = _toDoDataContext.Assignments.Where(id => id.User.Id.Equals(userId)).ToList();

            List<Assignment> list = listCompare.IsNullOrEmpty()? new List<Assignment>(): listCompare;
            foreach(Assignment item in list)
            {
                time = DateTime.Parse(item.DueDate.ToString());
                item.StrDate = item.DueDate.Date.ToString("d");
                item.StrTime = time.ToString("h:mm tt");
            }
            return list;
        }

        public async Task<List<Assignment>> GetAssignmentsAsync(string userId)
        {
            DateTime time;
            List<Assignment> listCompare = await _toDoDataContext.Assignments.Where(id => id.User.Id.Equals(userId)).ToListAsync();
            List<Assignment> list = listCompare.IsNullOrEmpty() ? new List<Assignment>() : listCompare;
            foreach (Assignment item in list)
            {
                time = DateTime.Parse(item.DueDate.ToString());
                item.StrDate = item.DueDate.Date.ToString("d");
                item.StrTime = time.ToString("h:mm tt");
            }
            return list;
        }

        public Assignment GetById(int id)
        {
            var context = _toDoDataContext;
            return !id.Equals(0) || !id.Equals(null) ? _toDoDataContext.Assignments.Find(id) : null;
            //return _toDoDataContext.Assignments.Find(id);
        }

        public async Task<Assignment> GetByIdAsync(int id)
        {
            return !id.Equals(0) || !id.Equals(null) ? await _toDoDataContext.Assignments.FindAsync(id) : null;
        }

        public async Task<IdentityUser> getUserAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public void UpdateAssignment(Assignment assignment)
        {
            _toDoDataContext.Assignments.Update(assignment);
            _toDoDataContext.SaveChanges();
        }

        public void CreateAssignment(Assignment assignment)
        {
            _toDoDataContext.Assignments.Add(assignment);
        }

        public void DeleteAssignment(Assignment assignment)
        {
            _toDoDataContext.Remove(assignment);
            _toDoDataContext.SaveChanges();

        }
    }
}
