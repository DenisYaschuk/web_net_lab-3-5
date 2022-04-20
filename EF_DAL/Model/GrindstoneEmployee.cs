using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_DAL.Model
{
    public class GrindstoneEmployee : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int GrindstoneId { get; set; }
        public Grindstone Grindstone { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
