using System.Xml.Serialization;

namespace BookBingoApi.Dtos.Goodreads.Shelf
{
    [XmlRoot("GoodreadsResponse", IsNullable = false)]
    public class ShelfResponse : IGoodreadsResponse<ShelfResults>
    {
        [XmlElement("reviews")]
        public ShelfResults Result { get; set; }
    }
}
