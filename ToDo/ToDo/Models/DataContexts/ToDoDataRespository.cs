using Microsoft.AspNetCore.Identity;
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
            List<Assignment> list = _toDoDataContext.Assignments.Where(id => id.User.Id.Equals(userId)).ToList();
            foreach(Assignment item in list)
            {
                time = DateTime.Parse(item.DueDate.ToString());
                item.StrDate = item.DueDate.Date.ToString("d");
                item.StrTime = time.ToString("h:mm tt");
            }
            return list;
        }

        public Assignment GetById(int id)
        {
            return !id.Equals(0) || !id.Equals(null) ? _toDoDataContext.Assignments.Find(id) : null;
            //return _toDoDataContext.Assignments.Find(id);
        }

        public async Task<IdentityUser> getUserAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
    }
}
