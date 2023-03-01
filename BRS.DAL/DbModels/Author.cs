using System;
using System.Collections.Generic;
using System.Text;

namespace BRS.DAL.DbModels
{
    public partial class Author : BaseEntity
    {
        public Author()
        {
            AuthorContacts = new HashSet<AuthorContact>();
            Books = new HashSet<Book>();
        }
        public string Name { get; set; }
        public virtual ICollection<AuthorContact> AuthorContacts { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
