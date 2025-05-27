using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Server.Library.Data;
using Server.Library.Repositories.Contracts;

namespace Server.Library.Repositories.Implementaions
{
    public class VacationRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Vacation>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.Vacations.FirstOrDefaultAsync(x => x.EmployeeId == id);
            if (item == null) return NotFound();
            appDbContext.Remove(item);

            await Commit();
            return Success();
        }

        public async Task<List<Vacation>> GetAll() => await appDbContext.
                                                  Vacations.
                                                  AsNoTracking().
                                                  ToListAsync();


        public async Task<Vacation?> GetById(int id) => await appDbContext.
            Vacations.
            FirstOrDefaultAsync(eid => eid.EmployeeId == id);


        public async Task<GeneralResponse> Insert(Vacation item)
        {
            appDbContext.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Vacation item)
        {
            var obj = await appDbContext.Vacations.FirstOrDefaultAsync(dr => dr.EmployeeId == item.Id);
            if (obj == null) return NotFound();
            obj.StartDate = item.StartDate;
            obj.NumberOfDays = item.NumberOfDays;
            obj.VacationTypeId = item.VacationTypeId;
            await Commit();
            return Success();
        }
        private static GeneralResponse NotFound() => new(false, "Sorry Vacation not found");
        private static GeneralResponse Success() => new(true, "Process Completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

    }
}
