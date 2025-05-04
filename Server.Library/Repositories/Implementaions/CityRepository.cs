
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Server.Library.Data;
using Server.Library.Repositories.Contracts;

namespace Server.Library.Repositories.Implementaions
{
    public class CityRepository(AppDbContext appDbContext):IGenericRepositoryInterface<City>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var city = await appDbContext.Cities.FindAsync(id);
            if (city == null) return NotFound();

            appDbContext.Remove(city);
            await Commit();
            return Success();
        }
        public async Task<List<City>> GetAll() =>
                    await appDbContext.Cities.AsNoTracking().
                            Include(city => city.Country).
                            ToListAsync();

        public async Task<City?> GetById(int id) =>
             await appDbContext.Cities.FindAsync(id);

        public async Task<GeneralResponse> Insert(City item)
        {
            if (!await CheckName(item.Name!)) return new(false, "Cirty Already added");
            appDbContext.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(City item)
        {
            var city = await appDbContext.Cities.FindAsync(item.Id);
            if (city is null) return NotFound();
            city.Name = item.Name;
            city.CountryId = item.CountryId;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry city not found");
        private static GeneralResponse Success() => new(true, "Process Completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var city = await appDbContext.Cities.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return city is null;
        }
    }
}
