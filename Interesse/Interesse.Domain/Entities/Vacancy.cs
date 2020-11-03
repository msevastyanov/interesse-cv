using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Interesse.Domain.Entities
{
    public class Vacancy : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
