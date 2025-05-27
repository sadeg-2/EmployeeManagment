
using System.Transactions;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Server.Library.Data;
using Server.Library.Repositories.Contracts;

namespace Server.Library.Repositories.Implementaions
{
    public class DoctorRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Doctor>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.Doctors.FirstOrDefaultAsync(x => x.EmployeeId == id);
            if (item == null) return NotFound();
            appDbContext.Doctors.Remove(item);

            await Commit();
            return Success();
        }

        public async Task<List<Doctor>> GetAll() => await appDbContext.
                                                  Doctors.
                                                  AsNoTracking().
                                                  ToListAsync();


        public async Task<Doctor?> GetById(int id) => await appDbContext.
            Doctors.
            FirstOrDefaultAsync(eid => eid.EmployeeId == id);
        

        public async Task<GeneralResponse> Insert(Doctor item)
        {
            appDbContext.Add(item);
            await Commit();
            return Success(); 
        }

        public async Task<GeneralResponse> Update(Doctor item)
        {
            var obj = await appDbContext.Doctors.FirstOrDefaultAsync(dr => dr.EmployeeId == item.Id);
            if (obj == null) return NotFound();
            obj.MedicalRecommendation = item.MedicalRecommendation;
            obj.MedicalDiagnose = item.MedicalDiagnose;
            obj.Date = item.Date;
            await Commit();
            return Success();
        }
        private static GeneralResponse NotFound() => new(false, "Sorry Docrtor not found");
        private static GeneralResponse Success() => new(true, "Process Completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

    }
}
