using GeoMed.Model.Main;
using GeoMed.Model.Setting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoMed.Model.Account
{
    public class GMUser : IdentityUser<int>
    {
        public GMUser()
        {
        }
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? DeleteDate { get; set; }

        [Column(TypeName = "int")]
        public int Gender { get; set; }  // enum (Gender)

        [Column(TypeName = "int")]
        public int UserType { get; set; }  // enum(UserTypes)

        [Column(TypeName = "datetime2")]
        public DateTime BirthDate { get; set; }

        public ICollection<Area> Areas { get; set; }

        public ICollection<PatientRecord> PatientRecords { get; set; }


    }
}
