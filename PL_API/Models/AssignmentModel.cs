namespace PL_API.Models
{
    public class AssignmentModel
    {
        public int Id { get; set; }


        public int GrindstoneId { get; set; }     
        public string Description { get; set; }
        public double TimeToFinish { get; set; }
        public int Priority { get; set; }
        public int StatusId { get; set; }


        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TeamId { get; set; }
    }
}
