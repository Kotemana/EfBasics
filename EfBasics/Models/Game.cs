using System.Text.Json.Serialization;

namespace EfBasics.Models
{
    public class Game: EntityBase, IEntityBase
    {
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<LarpPlayer> LarpPlayers { get; set; }

    }
}