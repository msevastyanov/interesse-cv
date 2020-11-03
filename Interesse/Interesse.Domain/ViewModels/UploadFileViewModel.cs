using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interesse.Domain.ViewModels
{
    public class UploadFileViewModel
    {
        public string Folder { get; set; }
        public IFormFile File { get; set; }
    }
}
