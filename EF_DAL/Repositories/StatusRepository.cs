using EF_DAL.Context;
using EF_DAL.Model;

namespace EF_DAL.Repositories
{
    public class StatusRepository : Repository<Status>
    {
        public StatusRepository(TaskManagerContext context) : base(context)
        {
        }
    }
}
