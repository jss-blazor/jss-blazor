using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JssBlazor.Tracking.Models;
using Microsoft.Extensions.Logging;

namespace JssBlazor.Tracking.Services
{
    public class LoggerTrackingApi : ITrackingApi
    {
        private readonly ILogger<LoggerTrackingApi> _logger;

        public LoggerTrackingApi(ILogger<LoggerTrackingApi> logger)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task TrackEventAsync(ITrackingModel @event, TrackingRequestOptions options)
        {
            await TrackEventsAsync(new[] { @event }, options);
        }

        public async Task TrackEventsAsync(IEnumerable<ITrackingModel> events, TrackingRequestOptions options)
        {
            if (!events.Any())
            {
                LogEvent("No events", null);
            }

            var firstEvent = events.ElementAt(0);
            var eventName = events.Count() > 1 ? "Batch" : firstEvent.GetType().Name;
            await Task.Run(() => LogEvent(eventName, firstEvent));            
        }

        private void LogEvent(string eventName, object data = null)
        {
            var message = new StringBuilder($"Event fired: {eventName}");
            if (data != null)
            {
                message.Append($" with data: {data.ToString()}");
            }
            message.Append(".");
            var formattedMessage = FormatLogMessage(this, message.ToString());
            _logger.LogInformation(formattedMessage);
        }

        private static string FormatLogMessage(object owner, string message)
        {
            return $"[{owner.GetType().FullName}] {message}";
        }
    }
}
