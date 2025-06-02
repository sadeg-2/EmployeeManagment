using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Server.Library.Data;
using Server.Library.Repositories.Contracts;

namespace Server.Library.Repositories.Implementaions
{
    public class OverTimeTypeRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<OverTimeType>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.OverTimeTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            appDbContext.Remove(item);
            await Commit();
            return Success();
        }

        public async Task<List<OverTimeType>> GetAll() => await appDbContext.
                                                  OverTimeTypes.
                                                  AsNoTracking().
                                                  ToListAsync();


        public async Task<OverTimeType?> GetById(int id) => await appDbContext.
            OverTimeTypes.
            FirstOrDefaultAsync(eid => eid.Id == id);


        public async Task<GeneralResponse> Insert(OverTimeType item)
        {
            if (!await CheckName(item.Name!)) return new(false, "Over Time Type Already added");
            appDbContext.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(OverTimeType item)
        {
            var obj = await appDbContext.OverTimeTypes.FindAsync(item.Id);
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
            var overTimeType = await appDbContext.OverTimeTypes.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return overTimeType is null;
        }
    }
}
