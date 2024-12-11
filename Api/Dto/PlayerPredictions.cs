
namespace Api.Dto
{
    public class PlayerPredictions
    {
        public string ? playerName { get; set; } 
        public double predictedPoints { get; set; } = 0;
        public string ? percentageChange { get; set; }
        public string ? trend { get; set; }
        public double avgBonusPoints { get; set; } = 0;
        public double pointsPerWeek  { get; set; }  = 0;
        public string ? assistsPercentage { get; set; }
        public string ? goalsPercentage { get; set; }
        public string ? cleanSheetPercentage {  get; set; }
    }
}