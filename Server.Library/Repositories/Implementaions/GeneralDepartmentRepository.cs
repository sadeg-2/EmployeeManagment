
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Server.Library.Data;
using Server.Library.Repositories.Contracts;

namespace Server.Library.Repositories.Implementaions
{
    public class GeneralDepartmentRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<GeneralDepartment>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var dep = await appDbContext.GeneralDepartments.FindAsync(id);
            if (dep == null) return NotFound();

            appDbContext.Remove(dep);
            await Commit();
            return Success();
        } 

        public async Task<List<GeneralDepartment>> GetAll() =>
                    await appDbContext.GeneralDepartments.ToListAsync();
        
        public async Task<GeneralDepartment?> GetById(int id)=>
             await appDbContext.GeneralDepartments.FindAsync(id);

        

        public async Task<GeneralResponse> Insert(GeneralDepartment item)
        {
            if(!await CheckName(item.Name!)) return new(false,"Department Already added");
            appDbContext.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(GeneralDepartment item)
        {
            var dept = await appDbContext.GeneralDepartments.FindAsync(item.Id);
            if (dept is null) return NotFound();
            dept.Name = item.Name;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false,"Sorry department not found");
        private static GeneralResponse Success() => new(true,"Process Completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var dept = await appDbContext.GeneralDepartments.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return dept is null;
        }        
    }
}
