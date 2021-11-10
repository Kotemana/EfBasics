namespace EfBasics.Models
{
    public class LarpPlayer : EntityBase, IEntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LarpName { get; set; }
        public virtual List<Weapon> Weapons { get; set; }
        public virtual List<Game> Games { get; set; }
    }
}
