using System.Collections.Generic;

namespace BookBingoApi.Dtos.Goodreads
{
    public interface IPaginatedResults<TItem>
    {
        public string ResultsStart { get; set; }

        public string ResultsEnd { get; set; }

        public string TotalResults { get; set; }

        public List<TItem> Items { get; set; }
    }
}
