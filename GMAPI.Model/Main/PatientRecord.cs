using GeoMed.Model.Account;
using GeoMed.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Model.Main
{
    public class PatientRecord  : BaseModel
    {
        [Column(TypeName = "int")]
        public int DossierNumber { get; set; }   // for every year Dossiers Numbers reseting

        [Column(TypeName = "datetime2")]
        public DateTime EntranceDate { get; set; }

        [Column(TypeName = "int")]
        public int TrackRecordId { get; set; }

        [ForeignKey(nameof(GMUserId))]
        public GMUser GMUser { get; set; }

        [Column(TypeName = "int")]
        public int GMUserId { get; set; }

     
    }
}
