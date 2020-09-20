using System.Collections.Generic;
using System.Xml.Serialization;

namespace BookBingoApi.Dtos.Goodreads.Shelf
{
    public class ShelfBook
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlArray("authors")]
        [XmlArrayItem("author")]
        public List<ShelfAuthor> Authors { get; set; }

        [XmlElement("image_url")]
        public string CoverUri { get; set; }

        [XmlElement("small_image_url")]
        public string SmallCoverUri { get; set; }

        [XmlElement("publication_year")]
        public string OriginalPublicationYear { get; set; }

        // Need surrogate values because the XML deserialiser can't handle nullable ints...
        [XmlIgnore]
        public int? PublicationYear { get => int.TryParse(OriginalPublicationYear, out int value) ? value : null as int?; }
    }
}
