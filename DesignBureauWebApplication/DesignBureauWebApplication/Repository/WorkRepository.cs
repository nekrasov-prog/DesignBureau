using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class WorkRepository : IWorkRepository
    {
        private readonly ApplicationDbContext _context;
        public WorkRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Work Work)
        {
            _context.Add(Work);
            return Save();
        }

        public bool Update(Work Work)
        {
            _context.Update(Work);
            return Save();
        }

        public bool Delete(Work Work)
        {
            _context.Remove(Work);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<Work>> GetAll()
        {
            return await _context.Work
                .Include(p => p.Project)
                .Include(wd => wd.WorkDictionary)
                .ToListAsync();
        }

        public async Task<Work?> GetByIdAsync(int id)
        {
            return await _context.Work
                .Include(p => p.Project)
                .Include(wd => wd.WorkDictionary)
                .FirstOrDefaultAsync(w => w.WorkId == id);
        }

        public async Task<Work?> GetByIdPlusDataAsync(int id)
        {
            return await _context.Work
                .Include(w => w.Assignments)
                    .ThenInclude(a => a.AssignmentDictionary)
                .Include(w => w.WorkHistories)
                .SingleOrDefaultAsync(w => w.WorkId == id);
        }

        public async Task<IEnumerable<Work>> GetAllWorksByProject(int projectId)
        {
            return await _context.Work.Where(w => w.Project.ProjectId == projectId).ToListAsync();
        }

        public async Task<List<WorkDictionary>> GetAllWorkDictionaries()
        {
            return await _context.WorkDictionary.ToListAsync();
        }

        public async Task<int> GetCompletedAssignmentsCountAsync(int workId)
        {
            return await _context.Assignment
                .Where(w => w.WorkId == workId)
                .OrderByDescending(w => w.EndDate)
                .Select(a => a.AssignmentHistories.OrderByDescending(ah => ah.CreatedAt)
                    .FirstOrDefault().AssignmentStatus)
                .CountAsync(status => status == AssignmentStatus.Completed);
        }

        public async Task<int> GetTotalAssignmentsCount(int workId)
        {
            var work = await _context.Work
                .Include(p => p.Assignments)
                .FirstOrDefaultAsync(p => p.WorkId == workId);

            if (work == null)
            {
                throw new ArgumentException($"Project with ID {workId} not found");
            }

            return work.Assignments.Count;
        }
    }
}
