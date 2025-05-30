using Azure.AI.TextAnalytics;
using Azure;

namespace ChatAppTestAzure.Services
{
    public class SentimentService
    {
        private readonly TextAnalyticsClient _client;

        public SentimentService(IConfiguration config)
        {
            var endpoint = new Uri(config["Azure:LanguageService:Endpoint"]);
            var credential = new AzureKeyCredential(config["Azure:LanguageService:Key"]);

            _client = new TextAnalyticsClient(endpoint, credential);
        }

        public async Task<string> AnalyzeSentimentAsync(string message)
        {
            var response = await _client.AnalyzeSentimentAsync(message);
            return response.Value.Sentiment.ToString().ToLower(); // "positive", "neutral", "negative"
        }
    }
}
