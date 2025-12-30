using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TableTennisBooking.Models
{
    public class Tables
    {
        [Key]
        public int TableId { get; internal set; }
        public string TableName { get; set; }

        public string TableDescription { get; set; }


        [DefaultValue(true)]
        public bool IsAvailable { get; set; }

    }
}
