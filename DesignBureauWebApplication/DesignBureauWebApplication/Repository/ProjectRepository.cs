using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using DesignBureauWebApplication.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DesignBureauWebApplication.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Project project)
        {
            _context.Add(project);
            return Save();
        }

        public bool Update(Project project)
        {
            _context.Update(project);
            return Save();
        }

        public bool Delete(Project project)
        {
            _context.Remove(project);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _context.Project.ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _context.Project.FirstOrDefaultAsync(p => p.ProjectId == id);
        }

        public async Task<Project> GetByIdPlusDataAsync(int id)
        {
            return await _context.Project
                .Include(p => p.Works)
                    .ThenInclude(w => w.WorkDictionary)
                .Include(p => p.ProjectHistories)
                .SingleOrDefaultAsync(p => p.ProjectId == id);
        }

        public async Task<int> GetCompletedWorksCountAsync(int projectId)
        {
            return await _context.Work
                .Where(w => w.ProjectId == projectId)
                .OrderByDescending(w => w.PlanOverDate)
                .Select(w => w.WorkHistories.OrderByDescending(wh => wh.CreatedAt)
                    .FirstOrDefault().WorkStatus)
                .CountAsync(status => status == WorkStatus.Completed);
        }

        public async Task<int> GetTotalWorksCount(int projectId)
        {
            var project = await _context.Project
                .Include(p => p.Works)
                .FirstOrDefaultAsync(p => p.ProjectId == projectId);

            if (project == null)
            {
                throw new ArgumentException($"Project with ID {projectId} not found");
            }

            return project.Works.Count;
        }
    }
}
