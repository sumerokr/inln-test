using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace inln_test.Models
{
    public class ImportedModel
    {
        [Key]
        public int ImportedModelId { get; set; }
        [DataType(DataType.Date)]
        public DateTime ImportedDate { get; set; }
    }
}