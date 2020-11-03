using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Interesse.Domain.Entities
{
    public class Photo : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
