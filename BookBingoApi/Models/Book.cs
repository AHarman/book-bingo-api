using System;
using System.Collections.Generic;

namespace BookBingoApi.Models
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public int PublicationMonth { get; set; }
        public int PublicationDay { get; set; }
        public Uri CoverUri { get; set; }
        public Uri SmallCoverUri { get; set; }
        public IEnumerable<Author> Authors { get; set; }
    }
}
