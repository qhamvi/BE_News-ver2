using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class Position
    {
        public int PositionId { get; set; }
        [Required]
        [MinLength(1)]
        public string PositionName { get; set; }
    }
}
