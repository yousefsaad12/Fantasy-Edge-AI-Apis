

// namespace Api.Services
// {
//     public class DataFetchingBackgroundService : BackgroundService
//     {   
//         private readonly ILogger<DataFetchingBackgroundService> _logger;
//         private readonly IServiceProvider _serviceProvider;
//         private int _currentWeek;
//         public DataFetchingBackgroundService(ILogger<DataFetchingBackgroundService> logger, IServiceProvider serviceProvider)
//         {
//             _logger = logger;
//             _serviceProvider = serviceProvider;
//             _currentWeek = 6;
//         }
//         protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//         {
//             _logger.LogInformation("Data Fetching Background Service is starting.");

//             while(!stoppingToken.IsCancellationRequested)
//             {
//                 var nextSaturday = GetNextSaturday();
//                 var delayUntilSaturday = nextSaturday - DateTime.Now;

//                 _logger.LogInformation("Next data fetch will be on {NextSaturday} (in {DelayHours} hours).", 
//                 nextSaturday, delayUntilSaturday.TotalHours);

//                 await Task.Delay(delayUntilSaturday, stoppingToken);

//                 try
//                 {
//                     _logger.LogInformation("Fetching data for week {Week}...", _currentWeek);

//                     using (var scope = _serviceProvider.CreateScope())
//                     {
//                         var fetchingService = scope.ServiceProvider.GetRequiredService<IFetchingService>();
                    
                        
//                         await fetchingService.FetchDataAsync(_currentWeek);
//                     }

//                     _logger.LogInformation("Data successfully fetched for week {Week}.", _currentWeek);

//                     // Increment the week counter for the next fetch
//                     _currentWeek++;
//                 }

//                 catch (Exception ex)
//                 {
//                     _logger.LogError(ex, "An error occurred while fetching data.");
//                 }

//                 // Wait for a week before the next execution (next Saturday)
//                     await Task.Delay(TimeSpan.FromDays(7), stoppingToken);
//             }
//         }
        


//          private DateTime GetNextSaturday()
//         {
//             var today = DateTime.Now;
//             int daysUntilSaturday = ((int)DayOfWeek.Saturday - (int)today.DayOfWeek + 7) % 7;

//             // If today is Saturday and the time has passed, wait until the next Saturday
//             if (daysUntilSaturday == 0 && today.TimeOfDay.Hours >= 0)
//             {
//                 daysUntilSaturday = 7;
//             }

//             return today.AddDays(daysUntilSaturday).Date; // Set the time to midnight (start of the day)
//         }
//     }
// }