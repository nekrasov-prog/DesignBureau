using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace DesignBureauWebApplication.Repository
{
    public class ProjectHistoryRepository : IProjectHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(ProjectHistory ProjectHistory)
        {
            _context.Add(ProjectHistory);
            return Save();
        }

        public bool Update(ProjectHistory ProjectHistory)
        {
            _context.Update(ProjectHistory);
            return Save();
        }

        public bool Delete(ProjectHistory ProjectHistory)
        {
            _context.Remove(ProjectHistory);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<ProjectHistory>> GetAll()
        {
            return await _context.ProjectHistory.Include(p => p.Project).ToListAsync();
        }

        public async Task<ProjectHistory> GetByIdAsync(int id)
        {
            return await _context.ProjectHistory.Include(p => p.Project).FirstOrDefaultAsync(ph => ph.ProjectHistoryId == id);
        }

        public async Task<ProjectStatus> GetLastProjectStatus(int id)
        {
            return await _context.Project.Where(p => p.ProjectId == id)
                .SelectMany(p => p.ProjectHistories)
                .OrderByDescending(ph => ph.CreatedAt)
                .Select(ph => ph.ProjectStatus)
                .FirstOrDefaultAsync();
        }

        public async Task<ProjectStatus> GetPreviousProjectStatus(int id)
        {
            return await _context.Project
                .Where(p => p.ProjectId == id)
                .SelectMany(p => p.ProjectHistories)
                .OrderByDescending(ph => ph.CreatedAt)
                .Skip(1) // пропустить последний статус
                .Select(ph => ph.ProjectStatus)
                .FirstOrDefaultAsync();
        }
    }
}
