﻿using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace Server.Library.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }

        // GeneralDepartments / Departments / Branches
        public DbSet<GeneralDepartment> GeneralDepartments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Branch> Branches { get; set; }

        // Country / City / Town
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Town> Towns { get; set; }
        // Authentication / Role / SystemRole 
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<RefreshTokenInfo> RefreshTokenInfos { get; set; }

        // Other / Vacation / Sanction / Doctor / OverTime
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<VacationType> VacationTypes { get; set; }
        public DbSet<Sanction> Sanctions { get; set; }
        public DbSet<SanctionType> SanctionTypes { get; set; }
        public DbSet<OverTime> OverTimes { get; set; }
        public DbSet<OverTimeType> OverTimeTypes { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

    }
}
