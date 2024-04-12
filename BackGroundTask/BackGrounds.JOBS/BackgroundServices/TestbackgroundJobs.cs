using Microsoft.Extensions.Hosting;

namespace BackGrounds.JOBS.BackgroundServices
{
    public class TestbackgroundJobs : BackgroundService
    {

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                GenerateFile();

                // Wait for one minute before generating the next files
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
          
        }

        private void GenerateFile()
        {
            try
            {
                // Generate a file with current timestamp as the file name
                string fileName = $"generated_file_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
               /* string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                // Write content to the file
                string content = "This is a generated file.";
                File.WriteAllText(filePath, content);
*/
                Console.WriteLine($"File generated: {fileName}");
            }
            catch (Exception ex)
            {
                // Handle failure case
                Console.WriteLine($"Error generating file: {ex.Message}");
            }
        }
    }
}
