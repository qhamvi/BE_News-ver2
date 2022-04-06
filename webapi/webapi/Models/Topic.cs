using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
         [Required]
        [MinLength(1)]
        public string TopicName { get; set; }
    }
}
