using System.Xml.Serialization;

namespace BookBingoApi.Dtos.Goodreads.Shelf
{
    public class ShelfReview
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("started_at")]
        public string BookStarted { get; set; }

        [XmlElement("read_at")]
        public string BookRead { get; set; }

        [XmlElement("date_added")]
        public string Added { get; set; }

        [XmlElement("date_updated")]
        public string LastUpdated { get; set; }

        [XmlElement("book")]
        public ShelfBook Book { get; set; }
    }
}
