
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Server.Library.Data;
using Server.Library.Repositories.Contracts;

namespace Server.Library.Repositories.Implementaions
{
    public class EmployeeRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Employee>
    {
        public async Task<List<Employee>> GetAll()
        {
            var employees = await appDbContext.Employees
                .AsNoTracking()
                .Include(e => e.Town)
                .ThenInclude(town => town.City)
                .ThenInclude(city => city.Country)
                .Include(e => e.Branch)
                .ThenInclude(branch => branch.Department)
                .ThenInclude(dep => dep.GeneralDepartment)
                .ToListAsync();
            return employees;
        }

        public async Task<Employee?> GetById(int id)
        {
            var emp = await appDbContext.Employees
                .Include(e => e.Town)
                .ThenInclude(town => town.City)
                .ThenInclude(city => city.Country)
                .Include(e => e.Branch)
                .ThenInclude(branch => branch.Department)
                .ThenInclude(dep => dep.GeneralDepartment)
                .FirstOrDefaultAsync(emp => emp.Id == id);
            if (emp == null) return null;
            return emp;
        }

        public async Task<GeneralResponse> Insert(Employee item)
        {
            if (! await CheckName(item.Name!)) return new GeneralResponse(false, "Employee already added");
            appDbContext.Employees.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Employee item)
        {
            var findUser = await appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == item.Id);
            if (findUser is null) return new GeneralResponse(false,"Employee does not exist");
            findUser.Name = item.Name;
            findUser.Id = item.Id;
            findUser.JobName = item.JobName;
            findUser.Other = item.Other;
            findUser.Photo = item.Photo;
            findUser.TownId = item.TownId;
            findUser.Address = item.Address;
            findUser.BranchId   = item.BranchId;
            findUser.CivilId = item.CivilId;
            findUser.FileNumber = item.FileNumber;
            findUser.TelephoneNumber = item.TelephoneNumber;
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> DeleteById(int id)
        {
            var emp = await appDbContext.Employees.FirstOrDefaultAsync(emp => emp.Id == id);
            if (emp == null) return NotFound();
            appDbContext.Employees.Remove(emp);
            await Commit();
            return Success();
        }
        private static GeneralResponse NotFound() => new(false, "Sorry Employee not found");
        private static GeneralResponse Success() => new(true, "Process Completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var employee = await appDbContext.Employees.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return employee is null;
        }
    }
}
