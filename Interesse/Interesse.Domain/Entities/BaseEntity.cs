using System;
using System.Collections.Generic;
using System.Text;

namespace Interesse.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsRemoved { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ChangedDate { get; set; }
    }
}
