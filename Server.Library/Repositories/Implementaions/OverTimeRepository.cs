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
    public class OverTimeRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<OverTime>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.OverTimes.FirstOrDefaultAsync(x => x.EmployeeId == id);
            if (item == null) return NotFound();
            appDbContext.Remove(item);

            await Commit();
            return Success();
        }

        public async Task<List<OverTime>> GetAll() => await appDbContext.
                                                  OverTimes.
                                                  AsNoTracking().
                                                  ToListAsync();


        public async Task<OverTime?> GetById(int id) => await appDbContext.
            OverTimes.
            FirstOrDefaultAsync(eid => eid.EmployeeId == id);


        public async Task<GeneralResponse> Insert(OverTime item)
        {
            appDbContext.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(OverTime item)
        {
            var obj = await appDbContext.OverTimes.FirstOrDefaultAsync(dr => dr.EmployeeId == item.EmployeeId);
            if (obj == null) return NotFound();
            obj.StartDate = item.StartDate;
            obj.EndTime  = item.EndTime;
            obj.OverTimeTypeId = item.OverTimeTypeId;
            await Commit();
            return Success();
        }
        private static GeneralResponse NotFound() => new(false, "Sorry Over Time not found");
        private static GeneralResponse Success() => new(true, "Process Completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();
    }
}
