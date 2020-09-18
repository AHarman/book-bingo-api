using System.Xml.Serialization;

namespace BookBingoApi.Dtos.Goodreads.Search.Books
{
    public class BookSearchAuthor
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
    }
}