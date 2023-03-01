using System;
using System.Collections.Generic;
using System.Text;

namespace BRS.DAL.DbModels
{
    public partial class Book : BaseEntity
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public int PublisherId { get; set; }
        public int AuthorId { get; set; }
        public virtual BookCategory Category { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual Author Author { get; set; }
    }
}
