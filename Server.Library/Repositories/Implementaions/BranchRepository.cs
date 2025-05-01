
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Server.Library.Data;
using Server.Library.Repositories.Contracts;

namespace Server.Library.Repositories.Implementaions
{
    public class BranchRepository(AppDbContext appDbContext):IGenericRepositoryInterface<Branch>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var branch = await appDbContext.Branches.FindAsync(id);
            if (branch == null) return NotFound();

            appDbContext.Remove(branch);
            await Commit();
            return Success();
        }
        public async Task<List<Branch>> GetAll() =>
                    await appDbContext.Branches.ToListAsync();
        public async Task<Branch?> GetById(int id) =>
             await appDbContext.Branches.FindAsync(id);



        public async Task<GeneralResponse> Insert(Branch item)
        {
            if (!await CheckName(item.Name!)) return new(false, "Branch Already added");
            appDbContext.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Branch item)
        {
            var dept = await appDbContext.Departments.FindAsync(item.Id);
            if (dept is null) return NotFound();
            dept.Name = item.Name;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry Branch not found");
        private static GeneralResponse Success() => new(true, "Process Completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var dept = await appDbContext.Departments.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return dept is null;
        }
    }
}
