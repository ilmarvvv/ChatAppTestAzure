﻿namespace ChatAppTestAzure.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Text { get; set; }
        public string Sentiment { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
