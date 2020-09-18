using System.Xml.Serialization;

namespace BookBingoApi.Dtos.Goodreads.Search.Books
{
    [XmlType("work")]
    public class BookSearchWork
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("best_book")]
        public BookSearchBook Book { get; set; }

        // Need surrogate values because the XML deserialiser can't handle nullable ints...
        [XmlIgnore]
        public int? PublicationYear { get => int.TryParse(OriginalPublicationYear, out int value) ? value : null as int?; }

        [XmlIgnore]
        public int? PublicationMonth { get => int.TryParse(OriginalPublicationMonth, out int value) ? value : null as int?; }

        [XmlIgnore]
        public int? PublicationDay { get => int.TryParse(OriginalPublicationDay, out int value) ? value : null as int?; }

        [XmlElement("original_publication_year")]
        public string OriginalPublicationYear { get; set; }
        
        [XmlElement("original_publication_month", IsNullable = true)]
        public string OriginalPublicationMonth { get; set; }
        
        [XmlElement("original_publication_day", IsNullable = true)]
        public string OriginalPublicationDay { get; set; }
        
    }
}
