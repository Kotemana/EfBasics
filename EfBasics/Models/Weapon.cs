namespace EfBasics.Models
{
    public class Weapon : EntityBase, IEntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Material Material { get; set; }
    }

}