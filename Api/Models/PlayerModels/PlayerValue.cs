namespace Api.Models.PlayerModels
{
    public class PlayerValue
    {
        public int PlayerValueId { get; set; }
        public int NowCost { get; set; }
        public int CostChangeEvent { get; set; }
        public int CostChangeStart { get; set; }
        public decimal SelectedByPercent { get; set; }
        public decimal  ValueForm { get; set; }
        public decimal ValueSeason { get; set; }
        public int PlayerId { get; set; }
        public Player player { get; set; }
    }
}