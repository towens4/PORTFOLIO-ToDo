using Microsoft.AspNetCore.Identity;
using ToDo.Models.DataModels;

namespace ToDo.Interfaces
{
    public interface IToDoRepository
    {
        public List<Assignment> GetAssignments(string userId);
        public List<Assignment> GetUncompletedAssignments(string userId);
        public List<Assignment> GetCompletedAssignments(string userId);
        public Assignment GetById(int id);
        public Task<IdentityUser> getUserAsync(string id);
        public Task<List<Assignment>> GetAssignmentsAsync(string userId);
        public Task<Assignment> GetByIdAsync(int id);
        public void UpdateAssignment(Assignment assignment);
        public void CreateAssignment(Assignment assignment);
        public void DeleteAssignment(Assignment assignment);
    }
}
