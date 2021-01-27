using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServerModels
{
    public class IdenServUser : IdentityUser<int>
    {
        [Column(TypeName = "datetime2")]
         public DateTime? DeletedDate { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        public bool IsDeleted => DeletedDate.HasValue;

    }
}
