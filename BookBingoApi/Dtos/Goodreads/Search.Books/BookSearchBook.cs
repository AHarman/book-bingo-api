using System.Xml.Serialization;

namespace BookBingoApi.Dtos.Goodreads.Search.Books
{
    public class BookSearchBook
    {
        [XmlElement("id")]
        public int Id { get; set; }
        
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("author")]
        public BookSearchAuthor Author { get; set; }

        [XmlElement("image_url")]
        public string CoverUri { get; set; }

        [XmlElement("small_image_url")]
        public string SmallCoverUri { get; set; }
    }
}