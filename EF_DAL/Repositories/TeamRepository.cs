using EF_DAL.Context;
using EF_DAL.Model;

namespace EF_DAL.Repositories
{
    public class TeamRepository : Repository<Team>
    {
        public TeamRepository(TaskManagerContext context) : base(context)
        {
        }
    }
}
