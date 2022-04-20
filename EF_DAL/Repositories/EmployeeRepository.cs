using EF_DAL.Context;
using EF_DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_DAL.Repositories
{
    public class EmployeeRepository : Repository<Employee>
    {
        public EmployeeRepository(TaskManagerContext context) : base(context)
        {
        }
    }
}
