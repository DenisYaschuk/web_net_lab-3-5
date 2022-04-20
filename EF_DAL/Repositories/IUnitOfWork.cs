using EF_DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_DAL.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Employee> Employee { get; }
        IRepository<Team> Team { get; }
        IRepository<Status> Status { get; }
        IRepository<Grindstone> Grindstone { get; }
        IRepository<GrindstoneEmployee> GrindstoneEmployee { get; }

        Task SaveChangesAsync();
    }
}
