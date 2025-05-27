using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Server.Library.Data;
using Server.Library.Repositories.Contracts;

namespace Server.Library.Repositories.Implementaions
{
    public class VacationTypeRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<VacationType>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.VacationTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            appDbContext.Remove(item);

            await Commit();
            return Success();
        }

        public async Task<List<VacationType>> GetAll() => await appDbContext.
                                                  VacationTypes.
                                                  AsNoTracking().
                                                  ToListAsync();


        public async Task<VacationType?> GetById(int id) => await appDbContext.
            VacationTypes.
            FirstOrDefaultAsync(eid => eid.Id == id);


        public async Task<GeneralResponse> Insert(VacationType item)
        {
            if (!await CheckName(item.Name!)) return new(false, "Vacation Type Already added");
            appDbContext.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(VacationType item)
        {
            var obj = await appDbContext.VacationTypes.FindAsync(item.Id);
            if (obj == null) return NotFound();
            obj.Name = item.Name;
            await Commit();
            return Success();
        }
        private static GeneralResponse NotFound() => new(false, "Sorry Sanction Type not found");
        private static GeneralResponse Success() => new(true, "Process Completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();
        private async Task<bool> CheckName(string name)
        {
            var vacationType = await appDbContext.VacationTypes.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return vacationType is null;
        }
    }
}
