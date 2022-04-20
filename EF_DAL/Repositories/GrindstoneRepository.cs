using EF_DAL.Context;
using EF_DAL.Model;

namespace EF_DAL.Repositories
{
    public class GrindstoneRepository : Repository<Grindstone>
    {
        public GrindstoneRepository(TaskManagerContext context) : base(context)
        {
        }
    }
}
