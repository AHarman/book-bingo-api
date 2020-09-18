using System;

namespace BookBingoApi.Models
{
    public class BookshelfEntry
    {
        public string Id { get; set; }
        public DateTime? BookStarted { get; set; }
        public DateTime? BookRead { get; set; }
        public DateTime? Added { get; set; }
        public DateTime? LastUpdated { get; set; }
        public Book Book { get; set; }
    }
}
