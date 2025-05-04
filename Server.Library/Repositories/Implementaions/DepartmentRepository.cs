using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Server.Library.Data;
using Server.Library.Repositories.Contracts;

namespace Server.Library.Repositories.Implementaions
{
    public class DepartmentRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Department>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var dep = await appDbContext.Departments.FindAsync(id);
            if (dep == null) return NotFound();

            appDbContext.Remove(dep);
            await Commit();
            return Success();
        }
        public async Task<List<Department>> GetAll() => await appDbContext.
            Departments.AsNoTracking().
            Include(gd => gd.GeneralDepartment).
            ToListAsync();

        public async Task<Department?> GetById(int id) =>
             await appDbContext.Departments.FindAsync(id);



        public async Task<GeneralResponse> Insert(Department item)
        {
            if (!await CheckName(item.Name!)) return new(false, "Department Already added");
            appDbContext.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Department item)
        {
            var dept = await appDbContext.Departments.FindAsync(item.Id);
            if (dept is null) return NotFound();
            dept.Name = item.Name;
            dept.GeneralDepartmentId = item.GeneralDepartmentId;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry department not found");
        private static GeneralResponse Success() => new(true, "Process Completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var dept = await appDbContext.Departments.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return dept is null;
        }
    }
}
