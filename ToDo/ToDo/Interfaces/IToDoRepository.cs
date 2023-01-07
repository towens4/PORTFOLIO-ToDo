using Microsoft.AspNetCore.Identity;
using ToDo.Models.DataModels;

namespace ToDo.Interfaces
{
    public interface IToDoRepository
    {
        public List<Assignment> GetAssignments(string userId);
        public Assignment GetById(int id);
        public Task<IdentityUser> getUserAsync(string id);
    }
}
