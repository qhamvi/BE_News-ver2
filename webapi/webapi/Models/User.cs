using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class User
    {
        
        public int UserId { get; set; }
        [Required]
        [MinLength(1)]
        public string UserName { get; set; }
        [Required]
        [MinLength(1)]
        public string Account { get; set; }
        [Required]
        [MinLength(1)]
        public string Password { get; set; }
        [Required]
        [MinLength(1)]
        public string Phone { get; set; }
        [Required]
        [MinLength(1)]
        public string Email { get; set; }
        [Required]
        [MinLength(1)]
        public string PhotoFileName { get; set; }
        [Required]
        [MinLength(1)]
        public string Position { get; set; }

    }
}
