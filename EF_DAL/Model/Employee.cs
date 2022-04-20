using System.ComponentModel.DataAnnotations;

namespace EF_DAL.Model
{
    public class Employee : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public int TeamId { get; set; }

        public Team Team { get; set; }

        public virtual ICollection<GrindstoneEmployee> Assignments { get; set; }

    }
}
