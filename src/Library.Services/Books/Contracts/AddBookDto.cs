using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Books.Contracts
{
    public class AddBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public byte MinAge { get; set; }
        public byte MaxAge { get; set; }
        public int CategoryId { get; set; }
    }
}
