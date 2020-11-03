using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Interesse.Domain.Entities
{
    public class Teacher : BaseEntity
    {
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }
        public string Photo { get; set; }
        [Required(ErrorMessage = "Введите должность")]
        public string Position { get; set; }
        public string Description { get; set; }
        [Required]
        public int Sort { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
    }
}
