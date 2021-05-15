namespace WpfIntro.Models
{
    public class MediaLog
    {
        public int Id { get; set; }
        public string LogText { get; set; }
        public MediaItem LogMediaItem { get; set; }

        public MediaLog(int id, string logText, MediaItem logMediaItem)
        {
            this.Id = id;
            this.LogText = logText;
            this.LogMediaItem = logMediaItem;
        }
    }
}