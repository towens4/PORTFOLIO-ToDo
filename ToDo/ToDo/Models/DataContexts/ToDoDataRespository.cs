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

            var list = _toDoDataContext.Assignments.Where(id => id.User.Id.Equals(userId)).ToList();
            return list;
        }

        public Assignment GetById(string id)
        {
            return !string.IsNullOrWhiteSpace(id) ? _toDoDataContext.Assignments.Find(Int32.Parse(id)) : null;
            //return _toDoDataContext.Assignments.Find(id);
        }

        public async Task<IdentityUser> getUserAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
    }
}
