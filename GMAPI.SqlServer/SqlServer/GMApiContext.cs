using GeoMed.Model.Account;
using GeoMed.Model.Main;
using GeoMed.Model.Setting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace GeoMed.SqlServer
{
    public class GMApiContext  : IdentityDbContext<GMUser ,GMRole , int,GMUserClaim ,
        GMUserRoles,GMUserLogin ,GMRoleClaim,GMUserToken>   
    {

        #region  == Constructer == 
        public GMApiContext(DbContextOptions<GMApiContext> options)
            : base(options)
        {

        }
        #endregion

        #region  == Entities == 
        public DbSet<Area> Areas { get; set; }
        public DbSet<Career> Careers { get; set; }
        public DbSet<PatientRecord> PatientRecords { get; set; }
        public DbSet<HealthCenter> HealthCenters { get; set; }
        #endregion


        #region  == Methods == 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region == Global Query Filter ==
           

            modelBuilder.Entity<Area>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<Career>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<HealthCenter>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<PatientRecord>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);


            #endregion
        }
      

        #endregion
    }
}
