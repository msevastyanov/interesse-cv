using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Interesse.Domain.Entities
{
    public class Post : BaseEntity
    {
        [Required(ErrorMessage = "Введите заголовок")]
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string PreviewImage { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Введите текст новости")]
        public string Text { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
    }
}
