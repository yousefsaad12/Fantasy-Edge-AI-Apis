namespace Api.Models.PlayerModels
{
    public class PlayerTransfer 
    {
        public int PlayerTransferId { get; set; }
        public int TransfersIn  { get; set; }
        public int TransfersInEvent  { get; set; }
        public int TransfersOut   { get; set; }
        public int TransfersOutEvent   { get; set; }
        public int PlayerId  { get; set; }
        public Player player  { get; set; }
    }
}