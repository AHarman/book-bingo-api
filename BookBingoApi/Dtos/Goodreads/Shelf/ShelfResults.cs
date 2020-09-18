using System.Collections.Generic;
using System.Xml.Serialization;

namespace BookBingoApi.Dtos.Goodreads.Shelf
{
    public class ShelfResults : IPaginatedResults<ShelfReview>
    {
        [XmlAttribute("start")]
        public string ResultsStart { get; set; }

        [XmlAttribute("end")]
        public string ResultsEnd { get; set; }

        [XmlAttribute("total")]
        public string TotalResults { get; set; }

        [XmlElement("review")]
        public List<ShelfReview> Items { get; set; }
    }
}
