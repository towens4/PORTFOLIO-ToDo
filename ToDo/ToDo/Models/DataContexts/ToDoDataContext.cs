using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDo.Models.DataModels;

namespace ToDo.Models.DataContexts
{
    public class ToDoDataContext:IdentityDbContext
    {
        public DbSet<Assignment> Assignments { get; set; }
        public ToDoDataContext(DbContextOptions<ToDoDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
