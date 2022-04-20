using System.ComponentModel.DataAnnotations;

namespace EF_DAL.Model
{
    public interface IEntity
    {
        [Key]
        int Id { get; set; }
    }
}
