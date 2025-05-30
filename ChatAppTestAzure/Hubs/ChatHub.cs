using ChatAppTestAzure.Data;
using ChatAppTestAzure.Models;
using ChatAppTestAzure.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatAppTestAzure.Hubs
{
    public class ChatHub : Hub
    {
        private readonly SentimentService _sentiment;
        private readonly ChatDbContext _db;

        public ChatHub(SentimentService sentiment, ChatDbContext db)
        {
            _sentiment = sentiment;
            _db = db;
        }

        public async Task SendMessage(string user, string message)
        {
            var sentiment = await _sentiment.AnalyzeSentimentAsync(message);

            // Додаємо повідомлення в базу
            var msg = new Message
            {
                User = user,
                Text = message,
                Sentiment = sentiment,
                Timestamp = DateTime.UtcNow
            };

            _db.Messages.Add(msg);
            await _db.SaveChangesAsync();

            // HTML-повідомлення для клієнтів
            string color = sentiment switch
            {
                "positive" => "green",
                "negative" => "red",
                _ => "gray"
            };

            string htmlMessage = $"<span style='color:{color}'><strong>{user}:</strong> {message}</span>";

            await Clients.All.SendAsync("ReceiveMessage", htmlMessage);
        }
    }
}