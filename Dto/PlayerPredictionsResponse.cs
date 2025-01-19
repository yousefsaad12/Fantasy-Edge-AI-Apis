
namespace Api.Dto
{
    public class PlayerPredictionsResponse
    {
        public string ? playerName { get; set; } 
        public double predictedPoints { get; set; } = 0;
        public string ? percentageChange { get; set; }
        public string ? trend { get; set; }
        public double avgBonusPoints { get; set; } = 0;
        public double pointsPerWeek  { get; set; }  = 0;
        public string ? assistsPercentage { get; set; } = "0";
        public string ? goalsPercentage { get; set; } = "0";
        public string ? cleanSheetPercentage {  get; set; } = "0";
    }
}