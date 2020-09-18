using System.Xml.Serialization;

namespace BookBingoApi.Dtos.Goodreads.Search.Books
{
    [XmlRoot("GoodreadsResponse", IsNullable = false)]
    public class BookSearchResponse : IGoodreadsResponse<BookSearchResults>
    {
        [XmlElement("search")]
        public BookSearchResults Result { get; set; }
    }
}
