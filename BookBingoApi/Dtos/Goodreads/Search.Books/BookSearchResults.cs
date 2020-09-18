using System.Collections.Generic;
using System.Xml.Serialization;

namespace BookBingoApi.Dtos.Goodreads.Search.Books
{
    public class BookSearchResults: IPaginatedResults<BookSearchWork>
    {
        [XmlElement("query")]
        public string Query { get; set; }

        [XmlElement("results-start")]
        public string ResultsStart { get; set; }

        [XmlElement("results-end")]
        public string ResultsEnd { get; set; }

        [XmlElement("total-results")]
        public string TotalResults { get; set; }

        [XmlArray("results")]
        public List<BookSearchWork> Items { get; set; }
    }
}
