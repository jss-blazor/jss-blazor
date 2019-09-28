using System.Collections.Generic;

namespace JssBlazor.Tracking.Models
{
    public class TrackingRequestOptions
    {
        /// <summary>
        /// Hostname of tracking service; e.g. http://my.site.core
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Relative path from host to tracking service. Default: /sitecore/api/jss/track
        /// </summary>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// An object of key:value pairs to be stringified and used as query string parameters.
        /// </summary>
        public IDictionary<string, string> QueryStringParams { get; set; }

        /// <summary>
        /// Type of tracking request action. Default: 'event'
        /// </summary>
        public string Action { get; set; } = "event";
    }
}
