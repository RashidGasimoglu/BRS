using System;
using System.Collections.Generic;
using System.Text;

namespace BRS.DAL.DbModels
{
    public partial class BookCategory : BaseEntity
    {
        public BookCategory()
        {
            Books = new HashSet<Book>();
        }
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
