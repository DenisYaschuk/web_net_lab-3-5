using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBL.DTO
{
    public class GrindstoneDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double TimeToFinish { get; set; }
        public int Priority { get; set; }
        public int StatusId { get; set; }
    }
}
