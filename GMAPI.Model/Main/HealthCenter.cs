using GeoMed.Model.Base;
using GeoMed.Model.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Model.Main
{
    public class HealthCenter  : BaseModel
    {
        [Column(TypeName = "nvarchar(50)")]
        public string Address { get; set; }

        [Column(TypeName = "int")]
        public  int SerialNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string PhoneNumber { get; set; }

        [ForeignKey(nameof(AreaId))]
        public Area Area { get; set; }

        public int AreaId { get; set; }
    }
}
