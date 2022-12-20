using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ToDo.Interfaces;
using ToDo.Models.DataModels;

namespace ToDo.Models.DataContexts
{
    public class ToDoDataRespository : IToDoRepository
    {
        public static ToDoDataContext _toDoDataContext;

        public ToDoDataRespository()
        {
        }

        public ToDoDataRespository(ToDoDataContext toDoDataContext)
        {
            _toDoDataContext = toDoDataContext;
        }

        public List<Assignment> GetAssignments(string userId)
        {
            return _toDoDataContext.Assignments.Where(id => id.User.Id.Equals(userId)).ToList();
        }
    }
}
