namespace LAeq_and_LEX.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ehs_laeq_db.setting")]
    public partial class setting
    {
        public int id { get; set; }

        public int interval { get; set; }

        [Required]
        [StringLength(25)]
        public string start_time { get; set; }

        [Required]
        [StringLength(25)]
        public string end_time { get; set; }

        public int during { get; set; }
    }
}
