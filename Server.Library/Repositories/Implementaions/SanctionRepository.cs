using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Server.Library.Data;
using Server.Library.Repositories.Contracts;

namespace Server.Library.Repositories.Implementaions
{
    public class SanctionRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Sanction>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.Sanctions.FirstOrDefaultAsync(x => x.EmployeeId == id);
            if (item == null) return NotFound();
            appDbContext.Remove(item);

            await Commit();
            return Success();
        }

        public async Task<List<Sanction>> GetAll() => await appDbContext.
                                                  Sanctions.
                                                  AsNoTracking().
                                                  ToListAsync();


        public async Task<Sanction?> GetById(int id) => await appDbContext.
            Sanctions.
            FirstOrDefaultAsync(eid => eid.EmployeeId == id);


        public async Task<GeneralResponse> Insert(Sanction item)
        {
            appDbContext.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Sanction item)
        {
            var obj = await appDbContext.Sanctions.FirstOrDefaultAsync(dr => dr.EmployeeId == item.Id);
            if (obj == null) return NotFound();
            obj.PunishmentDate = item.PunishmentDate;
            obj.Punishment = item.Punishment;
            obj.Date = item.Date;
            obj.SanactionType = item.SanactionType;
            await Commit();
            return Success();
        }
        private static GeneralResponse NotFound() => new(false, "Sorry Sanction not found");
        private static GeneralResponse Success() => new(true, "Process Completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();
    }
}
