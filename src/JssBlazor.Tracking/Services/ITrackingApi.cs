using System.Collections.Generic;
using System.Threading.Tasks;
using JssBlazor.Tracking.Models;

namespace JssBlazor.Tracking.Services
{
    public interface ITrackingApi
    {
        Task TrackEventAsync(ITrackingModel @event, TrackingRequestOptions options);
        Task TrackEventsAsync(IEnumerable<ITrackingModel> events, TrackingRequestOptions options);
    }
}
