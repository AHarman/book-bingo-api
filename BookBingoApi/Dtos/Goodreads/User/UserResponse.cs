
using System.Xml.Serialization;

namespace BookBingoApi.Dtos.Goodreads.User
{
    [XmlRoot("GoodreadsResponse", IsNullable = false)]
    public class UserResponse
    {
        [XmlElement("user")]
        public User User { get; set; }
    }
}
