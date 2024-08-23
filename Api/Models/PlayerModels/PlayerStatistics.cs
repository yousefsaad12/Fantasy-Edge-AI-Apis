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
      public decimal ExpectedGoalsPer90  { get; set; }  
      public decimal ExpectedAssistsPer90  { get; set; }  
      public decimal ExpectedGoalInvolvementsPer90 { get; set; }  
      public decimal ExpectedGoalsConcededPer90 { get; set; }  
      public decimal GoalsConcededPer90  { get; set; }  
      public decimal StartsPer90  { get; set; }  
      public decimal CleanSheetsPer90  { get; set; }
    
      public int PlayerId { get; set; }
      public Player player { get; set; }

    }
}