using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace hospitalwebapp.Models
{
    public class HospitalDbContext: DbContext
    {
        public HospitalDbContext() : base("MyConn") { }

        //Creating the Hospital Tables Logic
        public DbSet<StaffAccount> StaffAccounts { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Accounting> Accountings { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Laboratory> Laboratories { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Reception> Receptions { get; set; }
        public DbSet<PatientAccount> PatientAccounts { get; set; }
        public DbSet<DrugCategory> DrugCategories { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<LaboratoryTest> LaboratoryTests { get; set; }
        public DbSet<PatientTest> PatientTests { get; set; }
       

    }
}