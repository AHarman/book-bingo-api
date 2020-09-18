using System.Xml.Serialization;

namespace BookBingoApi.Dtos.Goodreads.Shelf
{
    public class ShelfAuthor
    {
        [XmlElement("name")]
        public string Name { get; set; }
    }
}