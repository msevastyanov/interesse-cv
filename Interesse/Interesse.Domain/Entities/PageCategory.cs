﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Interesse.Domain.Entities
{
    public class PageCategory : BaseEntity
    {
        [Required]
        public string Url { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string PreviewImage { get; set; }
        public int? ShowHomeCount { get; set; }
        [Required]
        public bool ShowOnMenuDropdown { get; set; }
        [Required]
        public bool ShowOnMainMenu { get; set; }
        [Required]
        public int Sort { get; set; }
        public CategoryRenderType RenderType { get; set; }
        public List<Page> Pages { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
    }

    public enum CategoryRenderType
    {
        [Display(Name = "Пост/статья")]
        Post,
        [Display(Name = "Курс")]
        Course
    }
}
