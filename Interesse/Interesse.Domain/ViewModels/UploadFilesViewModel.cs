using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interesse.Domain.ViewModels
{
    public class UploadFilesViewModel
    {
        public string Folder { get; set; }
        public IFormFileCollection Files { get; set; }
    }
}
