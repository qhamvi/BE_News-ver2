using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class Reason
    {
        public int ReasonId { get; set; }
         [Required]
        [MinLength(1)]
        public string Title { get; set; }
         [Required]
        [MinLength(1)]
        public string ReasonName { get; set; }
    }
}
