namespace Api.Models.PlayerModels
{
    public class ElementTypes
    {
        public int ElementTypeId { get; set; } 
        public string TypeName { get; set; }

        // Navigation Property
        public ICollection<Player> Players { get; set; }
    }
}