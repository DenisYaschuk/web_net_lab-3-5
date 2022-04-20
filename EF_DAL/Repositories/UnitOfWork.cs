using EF_DAL.Context;
using EF_DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskManagerContext _context;
        public IRepository<Employee> Employee { get; }
        public IRepository<Team> Team { get; }
        public IRepository<Status> Status { get; }
        public IRepository<Grindstone> Grindstone { get; }
        public IRepository<GrindstoneEmployee> GrindstoneEmployee { get; }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public UnitOfWork(IRepository<Employee> employees, IRepository<Team> teams, IRepository<Status> statuses, IRepository<Grindstone> grindstones, IRepository<GrindstoneEmployee> asignments, TaskManagerContext context)
        {
            Employee = employees;
            Team = teams;
            Status = statuses;
            Grindstone = grindstones;
            GrindstoneEmployee = asignments;
            _context = context;
        }
    }
}
