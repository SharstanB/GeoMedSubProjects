﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoMed.Model.Base
{
   public  class BaseModel
    {
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeleteDate { get; set; }
    }
}
