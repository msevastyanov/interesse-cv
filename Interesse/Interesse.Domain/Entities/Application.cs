using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Interesse.Domain.Entities
{
    public class Application : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Comment { get; set; }
        [Required]
        public bool IsRead { get; set; }
        [Required]
        public bool IsSuccess { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
