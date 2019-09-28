namespace JssBlazor.Tracking.Models
{
    public class Outcome : ITrackingModel
    {
        public string OutcomeId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal? MonetaryValue { get; set; }
    }
}
