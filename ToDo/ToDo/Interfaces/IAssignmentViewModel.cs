namespace ToDo.Interfaces
{
    public interface IAssignmentViewModel
    {
        string AssignmentDescription { get; set; }
        string AssignmentName { get; set; }
        DateTime DueDate { get; set; }
    }
}