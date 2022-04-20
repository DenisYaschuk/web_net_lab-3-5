using System.ComponentModel.DataAnnotations;

namespace EF_DAL.Model
{
    public class Grindstone : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public double TimeToFinish { get; set; }
        [Required]
        public int Priority { get; set; }
        [Required]
        public int StatusId { get; set; }

        public Status Status { get; set; }
        public virtual ICollection<GrindstoneEmployee> Assignments { get; set; }

    }
}
