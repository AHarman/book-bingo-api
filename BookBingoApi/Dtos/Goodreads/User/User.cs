using System.Xml.Serialization;

namespace BookBingoApi.Dtos.Goodreads.User
{
    public class User
    {
        [XmlElement("name")]
        public string Name { get; set; }
        
        [XmlAttribute(attributeName: "id")]
        public string Id { get; set; }
    }
}
