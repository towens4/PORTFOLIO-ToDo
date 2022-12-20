using ToDo.Models.DataModels;

namespace ToDo.Interfaces
{
    public interface IToDoRepository
    {
        public List<Assignment> GetAssignments(string userId);
    }
}
