using EF_DAL.Context;
using EF_DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace EF_DAL.Repositories
{
    public class GrindstoneEmployeeRepository : Repository<GrindstoneEmployee>
    {
        public GrindstoneEmployeeRepository(TaskManagerContext context) : base(context)
        {
        }
    }
}

