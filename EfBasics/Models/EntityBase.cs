using System.Text.Json.Serialization;

namespace EfBasics.Models
{
    public class EntityBase
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}
