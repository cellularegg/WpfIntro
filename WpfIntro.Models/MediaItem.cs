using System;

namespace WpfIntro.Models
{
    public class MediaItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime CreationTime { get; set; }

        public MediaItem(int id, string name, string url, DateTime creationTime)
        {
            Id = id;
            Name = name;
            Url = url;
            CreationTime = creationTime;
        }
    }
}