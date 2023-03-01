using System;
using System.Collections.Generic;
using System.Text;

namespace BRS.DAL.DbModels
{
    public partial class AuthorContact : BaseEntity
    {
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}
