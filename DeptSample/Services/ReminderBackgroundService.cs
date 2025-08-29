using DeptSample.Business.Interfaces;
using DeptSample.Business.Services;

namespace DeptSample.Services
{
    public class ReminderBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _provider;
       

        public ReminderBackgroundService(IServiceProvider provider)
        {
            _provider = provider;
           
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _provider.CreateScope();
                var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();
                var _reminders = scope.ServiceProvider.GetRequiredService<IReminderService > ();

                var due = await _reminders.GetDueAsync(DateTime.Now);

                foreach (var reminder in due)
                {
                    emailService.SendReminderEmail(reminder.Email,"⏰ Reminder: " + reminder.Title, "Your scheduled reminder.");
                    reminder.IsSent = true;
                   await _reminders.UpdateAsync(reminder);
                }

                
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
