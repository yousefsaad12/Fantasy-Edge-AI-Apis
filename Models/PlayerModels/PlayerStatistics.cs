namespace Api.Models.PlayerModels
{
    public class PlayerStatistics
    {
      public int PlayerStatisticsId { get; set; }  
      public decimal Influence  { get; set; }  
      public decimal Creativity  { get; set; }  
      public decimal Threat { get; set; }  
      public decimal IctIndex  { get; set; }  
      public decimal ExpectedGoals { get; set; }  
      public decimal ExpectedAssists { get; set; }  
      public decimal ExpectedGoalInvolvements { get; set; }  
      public decimal ExpectedGoalsConceded { get; set; }  
      public int GameWeek { get; set; }      
      public int PlayerId { get; set; }

      // Navigation Properties

      public Player player { get; set; }

    }
}